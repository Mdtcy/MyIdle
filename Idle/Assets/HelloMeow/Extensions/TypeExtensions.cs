/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-17 19:07:48
 * @modify date 2020-07-17 19:07:48
 * @desc [description]
 */

using System;
using System.Collections.Generic;

namespace HM.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断类型是否为List
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        /// <summary>
        /// 判断类型是否为HashSet
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsHashSet(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>);
        }
    }
}