using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HM.GameBase
{
    [Serializable]
    public class ItemParams : IHMPooledObject, IDisposable
    {
        #region FIELDS
        [SerializeField]
        private List<ItemData> values;
        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public int Count => values.Count;

        /// <summary>
        /// 添加item数据，不会与已有元素合并总数
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="num"></param>
        /// <param name="state"></param>
        public void AddNoCheck(int itemId, int num, ItemState state = ItemState.Default)
        {
            values.Add(new ItemData(itemId, num, state));
        }

        /// <summary>
        /// 添加item数据
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="num"></param>
        /// <param name="state"></param>
        public void Add(int itemId, int num, ItemState state = ItemState.Default)
        {
            for (int i = 0; i < values.Count; i++)
            {
                var itemData = values[i];
                // 相同ItemId已存在，直接更新
                if (itemData.ItemId == itemId)
                {
                    values[i] = new ItemData(itemData.ItemId, itemData.Num + num, ItemState.Default);
                    return;
                }
            }

            values.Add(new ItemData(itemId, num, state));
        }

        public List<ItemData>.Enumerator GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public ItemParams()
        {
            values = ListPool<ItemData>.Claim(8);
            HMLog.LogVerbose($"create new itemParams");
        }

        public void Clear()
        {
            values.Clear();
        }

        public ItemParams Clone()
        {
            var clone = Create();
            clone.values.AddRange(values);
            return clone;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"[ItemParams Count = {values.Count}, Items = ");

            foreach (var data in values)
            {
                sb.Append(data.ToString());
                sb.Append(",");
            }

            sb.Append("]");
            return sb.ToString();
        }

        public void Dispose()
        {
            HMLog.LogDebug("Dispose itemParams");
            this.ReleaseToPool();
        }

        public void OnEnterPool()
        {
            HMLog.LogVerbose("ItemParams on enter pool");
            values.Clear();
        }

        public void Sort(IComparer<ItemData> comparer)
        {
            values.Sort(comparer);
        }

        #endregion

        #region PROTECTED METHODS
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS

        public static ItemParams Create()
        {
            return ObjectPool<ItemParams>.Claim();
        }

        #endregion

    }
}
