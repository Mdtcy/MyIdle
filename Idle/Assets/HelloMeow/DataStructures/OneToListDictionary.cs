/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-03-20 09:44:46
 * @modify date 2019-03-20 09:44:46
 * @desc [实现了一对多关系的字典结构，value是List类型，目前只实现了必需的几个功能，待补充]
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM.DataStructures
{
    /// <summary>
    /// 一对多字典
    /// </summary>
    /// <typeparam name="TKey">key类型</typeparam>
    /// <typeparam name="TValue">value类型，不必指定为List<T>，直接指定T即可</typeparam>
    [Serializable]
    public class OneToListDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, List<TValue>>>
    {
        #region FIELDS
        // 内部使用Dictionary
        [SerializeField]
        private Dictionary<TKey, List<TValue>> dict = new Dictionary<TKey, List<TValue>>();
		#endregion

		#region PROPERTIES
		#endregion

		#region PUBLIC METHODS

		/// <summary>
		/// 添加一组key/value
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            this[key].Add(value);
        }

		/// <summary>
		/// 设置key和一组values
		/// </summary>
		/// <param name="key"></param>
		/// <param name="values"></param>
		public void Set(TKey key, List<TValue> values)
		{
			this[key].Clear();
			this[key].AddRange(values);
		}

		/// <summary>
		/// 返回指定key对应的一组value，如果不存在返回空数组
		/// </summary>
		/// <param name="key"></param>
		public List<TValue> this[TKey key]
        {
            get
            {
                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, new List<TValue>());
                }
                return dict[key];
            }
        }

		/// <summary>
		/// 清空全部内容
		/// </summary>
		public void Clear()
		{
			dict.Clear();
		}

		/// <summary>
		/// 实现IEnumerable接口
		/// </summary>
		/// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, List<TValue>>> GetEnumerator()
        {
            foreach (var pair in dict)
            {
                yield return pair;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 检查是否有指定key存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
	        return dict.ContainsKey(key);
        }
		#endregion

		#region PROTECTED METHODS
		#endregion

		#region PRIVATE METHODS
		#endregion

		#region STATIC METHODS
		#endregion
    }
}