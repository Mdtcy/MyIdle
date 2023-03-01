/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月7日
 * @modify date 2023年2月7日
 * @desc [ListExtensions]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;

namespace XYZUtility.Extensions
{
    public static class ListExtensions
    {
        public static T Random<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                throw new IndexOutOfRangeException("列表至少需要一个元素才能调用Random()");
            }

            if (list.Count == 1)
            {
                return list[0];
            }

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T RandomOrDefault<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            return list.Random();
        }

        public static T PopLast<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var t = list[list.Count - 1];

            list.RemoveAt(list.Count - 1);

            return t;
        }
    }
}
#pragma warning restore 0649