/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-05-11 13:05:02
 * @modify date 2021-05-11 13:05:02
 * @desc [为在配置管理器导入导出HelloMeow类型提供序列化&反序列化支持]
 */

using System;
using HM.Extensions;
using HM.GameBase;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HM.EditorOnly.TypeParser
{
    public class HelloMeowTypeSerializer
    {
        // 如果为null，标记为NUL
        protected const string NullStr = "NUL";
        // 标记不会导出/导入的值
        private const string SkipToken = "SKP";

        /// <summary>
        /// 指定类型和变量转为字符串格式表示，用于导出到csv
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string ToString(Type type, object value)
        {
            if (value == null)
            {
                return NullStr;
            }

            if (type.IsSubclassOf(typeof(BaseConfig)))
            {
                // 如果是baseConfig子类，只导出id
                return ((BaseConfig) value).Id.ToString();
            }

            if (value is Vector2)
            {
                // (3, 2) -> (3|2)，
                return Encode(EditorJsonUtility.ToJson(value));
            }

            if (value is Enum)
            {
                return Convert.ToInt32(value).ToString();
            }

            if (type.IsValueType || value is string)
            {
                return value.ToString();
            }

            if (value is UnityEngine.Object someObject)
            {
                // return its path relative to Assets
                return AssetDatabase.GetAssetPath(someObject);
            }

            HMLog.LogVerbose($"{value.GetType()}");
            return SkipToken;
        }

        /// <summary>
        /// 尝试从csv读取值并转换为真实对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual bool TryToParse(Type type, string data, out object result)
        {
            if (data.Contains(SkipToken))
            {
                result = null;
                // skip this data
                return false;
            }

            if (data.Equals(NullStr))
            {
                result = null;
                return true;
            }

            if (type.IsSubclassOf(typeof(BaseConfig)))
            {
                int id = Convert.ToInt32(data);
                result = FindBaseConfig(id);
                return result != null;
            }

            if (type == typeof(int))
            {
                result = Convert.ToInt32(data);
                return true;
            }

            if (type == typeof(bool))
            {
                result = Convert.ToBoolean(data);
                return true;
            }

            if (type.IsSubclassOf(typeof(Enum)))
            {
                // toInt16 / toInt32取决于枚举类型的定义是否为short / int
                result = Convert.ToInt16(data);
                return true;
            }

            if (type == typeof(Vector2))
            {
                result = JsonUtility.FromJson<Vector2>(Decode(data));
                return true;
            }

            if (type == typeof(Single))
            {
                result = Convert.ToSingle(data);
                return true;
            }

            if (type == typeof(float) || type == typeof(double))
            {
                result = Convert.ToDouble(data);
                return true;
            }

            if (type.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                // for Object type, data is the path of actual asset.
                result = AssetDatabase.LoadAssetAtPath(data, type);
                return true;
            }

            if (type == typeof(string))
            {
                result = data.Replace("[LB]", "\n");
                return true;
            }

            result = data;
            return true;
        }

        // 导出为csv后，逗号会被视作默认的分隔符，为了方便csv，此处稍微麻烦处理一下，对逗号进行转义
        protected static string Encode(string text)
        {
            return text.Replace(",", ")#(").Replace("\"", "'");
        }

        // 恢复转义的逗号
        protected static string Decode(string text)
        {
            return text.Replace(")#(", ",").Replace("'", "\"");
        }

        // 根据itemId查找对应的配置(BaseConfig)
        protected static BaseConfig FindBaseConfig(int itemId)
        {
            return FindAsset<BaseConfig>($"{itemId}");
        }

        // 根据资源名查找对应的资源
        protected static T FindAsset<T>(string name) where T : Object
        {
            var list = AssetDatabase.FindAssets(name);
            return list.IsNullOrEmpty() ? null : AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(list[0]));
        }
    }
}