/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-05-11 14:05:43
 * @modify date 2021-05-11 14:05:43
 * @desc [支持三秋的自定义类型在配置导入导出中的序列化&反序列化]
 */

using System;
using HM.EditorOnly.TypeParser;
using NewLife.Defined.Condition;
using UnityEditor;

namespace NewLife.Config
{
    public class NewLifeTypeSerializer : HelloMeowTypeSerializer
    {
        /// <inheritdoc />
        public override string ToString(Type type, object value)
        {
            if (type == typeof(UnionCommandConfig))
            {
                var inst = (UnionCommandConfig) value;

                var dict = new SerializableDictionary<string, int> {{"CommandType", (int)inst.CommandType}, {"Num", inst.Num}, {"Target", inst.Target?.Id??0}};
                return Encode(EditorJsonUtility.ToJson(dict));
            }

            return base.ToString(type, value);
        }
    }
}