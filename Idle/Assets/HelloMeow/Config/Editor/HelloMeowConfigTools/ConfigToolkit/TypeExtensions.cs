/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-05-11 14:05:34
 * @modify date 2021-05-11 14:05:34
 * @desc [description]
 */

using System;
using System.Reflection;

namespace HM.EditorOnly
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 清空指定list对象的内容
        /// </summary>
        /// <param name="type"></param>
        /// <param name="list"></param>
        public static void ClearList(this Type type, object list)
        {
            var clearMethod = type.GetMethod("Clear");

            if (clearMethod == null)
            {
                return;
            }

            if (list != null)
            {
                clearMethod.Invoke(list, null);
            }
        }

        /// <summary>
        /// 返回指定类型的Item属性（必须为list类型）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo GetListItemPropertyInfo(this Type type)
        {
            return type.GetProperty("Item");
        }

        /// <summary>
        /// 给list添加一个新元素，默认为null
        /// </summary>
        /// <param name="type"></param>
        /// <param name="list"></param>
        public static void AppendItemToList(this Type type, object list)
        {
            var addMethod = type.GetMethod("Add");

            if (addMethod == null)
            {
                return;
            }

            addMethod.Invoke(list, new object[] { null });
        }
    }
}