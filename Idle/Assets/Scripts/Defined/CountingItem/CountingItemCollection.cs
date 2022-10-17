/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-03-02 14:07:46
 * @modify date 2022-03-02 14:07:46
 * @desc [CountingItem集合，仅支持在运行中使用]
 */

using System;
using System.Collections.Generic;
using HM;
using HM.Extensions;
using HM.GameBase;
using Zenject;

namespace NewLife.Defined
{
    public partial class CountingItemCollection : IHMPooledObject, IDisposable
    {
        private List<CountingItem> items;

        // 如果不需要调用Acquire()，可以使用Claim()
        public static CountingItemCollection Claim()
        {
            var inst = ObjectPool<CountingItemCollection>.Claim();
            inst.Initialize();

            return inst;
        }

        /// <inheritdoc />
        public void OnEnterPool()
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.ReleaseToPool();
                }

                items.ReleaseToPool();
            }

            items = null;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.ReleaseToPool();
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            items.Clear();
        }

        public CountingItem this[int key] => items.Count > key && key >= 0 ? items[key] : null;

        /// <summary>
        /// 包含的CountingItem数量
        /// </summary>
        public int Count => items.Count;

        public ListFastEnumerator<CountingItem> GetEnumerator()
        {
            return new ListFastEnumerator<CountingItem>(items);
        }

        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="item"></param>
        public void Add(CountingItem item)
        {
            HMLog.Assert(item != null && item.Item != null);
            items.Add(item.Clone());
        }

        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="item"></param>
        public void Add(BaseConfig config, int num)
        {
            HMLog.Assert(config != null);
            using var inst = CountingItem.Claim(config, num);
            items.Add(inst);
        }

        /// <summary>
        /// 添加一个物品，首先判断是否有同类物品，如果有则合并数量，没有直接添加
        /// </summary>
        /// <param name="item"></param>
        public void MergeOrAdd(CountingItem item)
        {
            HMLog.Assert(item != null && item.Item != null);

            foreach (var countingItem in items)
            {
                if (countingItem.Item == item.Item)
                {
                    countingItem.Num += item.Num;

                    return;
                }
            }

            Add(item);
        }

        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="item"></param>
        public void MergeOrAdd(BaseConfig config, int num)
        {
            HMLog.Assert(config != null);
            using var inst = CountingItem.Claim(config, num);
            MergeOrAdd(inst);
        }

        /// <summary>
        /// 批量添加物品（不合并）
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(List<CountingItem> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// 批量添加物品（合并）
        /// </summary>
        /// <param name="items"></param>
        public void AddRangeMerged(List<CountingItem> items)
        {
            foreach (var item in items)
            {
                MergeOrAdd(item);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<CountingItem> comparer)
        {
            items.Sort(comparer);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{items.ListToString()}";
        }

        private void Initialize()
        {
            HMLog.Assert(items == null);
            items = HM.GameBase.ListPool<CountingItem>.Claim(32);
        }

        public class Factory : IFactory<CountingItemCollection>
        {
            private readonly DiContainer container;

            public Factory(DiContainer container)
            {
                this.container = container;
            }

            public CountingItemCollection Create()
            {
                var inst = new CountingItemCollection();
                container.Inject(inst);

                inst.Initialize();

                return inst;
            }
        }
    }
}