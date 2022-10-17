/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-01-09 17:58:27
 * @modify date 2019-01-09 17:58:27
 * @desc [List扩展]
 */

#if ZSTRING
using MyString = Cysharp.Text.ZString;
#else
using MyString = System.String;
#endif
using System;
using System.Collections.Generic;
using HM.GameBase;
using UnityEngine;

namespace HM.Extensions
{
    /// <summary>
    /// List扩展
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 返回列表随机一个元素，如果列表长度为0，返回元素类型默认值
        /// </summary>
        /// <param name="list">列表对象</param>
        /// <typeparam name="T">元素类型</typeparam>
        /// <returns></returns>
        public static T Random<T>(this List<T> list)
        {
            return list.IsNullOrEmpty() ? default : list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Returns <c>true</c> if the list is either null or empty. Otherwise <c>false</c>.
        /// </summary>
        /// <param name="list">The list.</param>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list != null)
            {
                return list.Count == 0;
            }

            return true;
        }

        /// <summary>
        /// 跳过null值的add方法
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddNullSkipped<T>(this IList<T> list, T value) where T : class
        {
            if (value != null)
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// 增加元素，如果已存在则跳过
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static bool AddNoDuplicate<T>(this IList<T> list, T value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 返回列表的最后一个元素，如果列表长度为0，返回元素类型默认值
        /// </summary>
        /// <param name="list">列表对象</param>
        /// <typeparam name="T">元素类型</typeparam>
        /// <returns></returns>
        public static T LastOne<T>(this List<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return default;
            }

            return list[list.Count - 1];
        }

        /// <summary>
        /// 删除数组的最后一个元素
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveLast<T>(this List<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return;
            }

            list.RemoveAt(list.Count - 1);
        }

        /// <summary>
        /// 逆序访问数组元素
        /// </summary>
        /// <param name="list"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        public static void ReverseForEach<T>(this List<T> list, Action<T> callback)
        {
            if (callback == null)
            {
                return;
            }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                callback(list[i]);
            }
        }

        /// <summary>
        /// 随机并返回List中的一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">目标List</param>
        /// <param name="delete">返回后是否删除</param>
        /// <returns></returns>
        public static T RandomAndDelete<T>(this List<T> list)
        {
            var rand   = UnityEngine.Random.Range(0, list.Count);
            var result = list[rand];
            list.RemoveAt(rand);

            return result;
        }

        /// <summary>
        /// 比较并插入到第一个返回true的位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">目标List</param>
        /// <param name="target">想要插入的对象</param>
        /// <param name="onCompare">比较回调</param>
        public static void InsertByCompare<T>(this List<T> list, T target, Func<T, bool> onCompare)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (onCompare(list[i]))
                {
                    list.Insert(i, target);

                    return;
                }
            }

            list.Add(target);
        }

        /// <summary>
        /// 去除重复元素（前提是已排好序）
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveDuplicates<T>(this List<T> list)
        {
            int total = list.Count;
            int i     = 0;
            int j     = 1;

            while (j < total)
            {
                if (list[i].Equals(list[j]))
                {
                    list.RemoveAt(j);
                    total--;
                }
                else
                {
                    i = j;
                    j++;
                }
            }
        }

        /** Identical to ToArray but it uses ArrayPool<T> to avoid allocations if possible.
		 *
		 * Use with caution as pooling too many arrays with different lengths that
		 * are rarely being reused will lead to an effective memory leak.
		 */
        public static T[] ToArrayFromPool<T>(this List<T> list)
        {
            var arr = ArrayPool<T>.ClaimWithExactLength(list.Count);

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = list[i];
            }

            return arr;
        }

        /** Clear a list faster than List<T>.Clear.
         * It turns out that the List<T>.Clear method will clear all elements in the underlaying array
         * not just the ones up to Count. If the list only has a few elements, but the capacity
         * is huge, this can cause performance problems. Using the RemoveRange method to remove
         * all elements in the list does not have this problem, however it is implemented in a
         * stupid way, so it will clear the elements twice (completely unnecessarily) so it will
         * only be faster than using the Clear method if the number of elements in the list is
         * less than half of the capacity of the list.
         *
         * Hopefully this method can be removed when Unity upgrades to a newer version of Mono.
         */
        public static void ClearFast<T>(this List<T> list)
        {
            if (list.Count * 2 < list.Capacity)
            {
                list.RemoveRange(0, list.Count);
            }
            else
            {
                list.Clear();
            }
        }

        /// <summary>
        /// 释放list到ListPool
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void ReleaseToPool<T>(this List<T> list)
        {
            ListPool<T>.Release(list);
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            for (var i = list.Count - 1; i > 0; i--)
            {
                var j    = (int) Mathf.Floor(Mathf.Max(0, UnityEngine.Random.value - 0.0001f) * (i + 1));
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            return list;
        }

        /// <summary>
        /// 返回列表的第一个元素
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T First<T>(this List<T> list)
        {
            return list.IsNullOrEmpty() ? default : list[0];
        }

        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            var tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }

        /// <summary>
        /// Returns a pretty string representation of the given list. The resulting string looks something like
        /// <c>[a, b, c]</c>.
        /// </summary>
        public static string ListToString<T>(this List<T> source)
        {
            if (source == null)
            {
                return "null";
            }

            if (source.Count == 0)
            {
                return "[]";
            }

            if (source.Count == 1)
            {
                return MyString.Format("[{0}]", source[0]);
            }

#if ZSTRING
            using (var sb = MyString.CreateStringBuilder())
#else
            var sb = new System.Text.StringBuilder();
#endif
            {
                sb.AppendFormat("[({0})", source.Count);

                foreach (var s in source)
                {
                    sb.AppendFormat("{0}, ", s);
                }

                sb.Append("]");

                return sb.ToString();
            }
        }

        /// <summary>
        /// 效率更高的RemoveAt
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        public static void FastRemoveAt<T>(this List<T> list, int index)
        {
            if (list.IsNullOrEmpty() || index >= list.Count || index < 0)
            {
                return;
            }
            list.Swap(index, list.Count - 1);
            list.RemoveLast();
        }

        /// <summary>
        /// 快速删除指定元素(会导致元素顺序变化)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static void FastRemove<T>(this List<T> list, T item)
        {
            if (list.IsNullOrEmpty() || item == null)
            {
                return;
            }

            FastRemoveAt(list, list.IndexOf(item));
        }

        /// <summary>
        /// 从指定数组筛选符合条件的子集
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filteredList"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        public static void Filter<T>(this List<T> list, ref List<T> filteredList, Func<T, bool> predicate)
        {
            if (list.IsNullOrEmpty())
            {
                return;
            }

            if (predicate == null)
            {
                filteredList = list;
            }

            foreach (var item in list)
            {
                if (predicate(item))
                {
                    filteredList.Add(item);
                }
            }
        }
    }
}