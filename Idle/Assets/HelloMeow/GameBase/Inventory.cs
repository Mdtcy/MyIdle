using System;
using System.Collections.Generic;
using HelloMeow.Signal;
using HM.Extensions;
using HM.Interface;
using Zenject;

namespace HM.GameBase
{
    [Serializable]
    public class Inventory : PersistableObject
    {
        #region FIELDS
        [UnityEngine.SerializeField]
        private Dictionary<int, ItemBase> dictItems = new Dictionary<int, ItemBase>();

        // inject
        private SignalBus signalBus;

        #endregion

        #region PROPERTIES
        public bool IsDirty;
        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder("Inventory=[");
            foreach (var item in dictItems.Values)
            {
                sb.AppendLine(item.ToString());
            }
            sb.Append("]");
            return sb.ToString();
        }

        #region Modify

        public void RemoveItem(int itemid)
        {
            if (HasItem(itemid))
            {
                var item = GetItem(itemid);
                item.OnRemoved();
                dictItems.Remove(itemid);
            }
            IsDirty = true;
        }

        /// <summary>
        /// 删除所有保存的item
        /// </summary>
        public void Clear()
        {
            foreach (var item in dictItems.Values)
            {
                item.OnRemoved();
            }
            dictItems.Clear();
            IsDirty = true;
        }

        #endregion

        #region Query

        public bool HasItem(int itemId)
        {
            return dictItems.ContainsKey(itemId);
        }

        public ItemBase GetItem(int itemId)
        {
            return HasItem(itemId) ? dictItems[itemId] : null;
        }

        public T GetItem<T>(int itemId)where T : ItemBase
        {
            return HasItem(itemId) ? dictItems[itemId].ToType<T>() : null;
        }

        public void FilterItems<T>(Func<T, bool> fn, ref List<T> items) where T : ItemBase
        {
            foreach (var item in dictItems.Values)
            {
                if (item is T itemT && fn(itemT))
                {
                    items.Add(itemT);
                }
            }
        }

        public void FilterItems<T>(ref List<T> items) where T : ItemBase
        {

            foreach (var item in dictItems.Values)
            {
                if (item is T itemT)
                {
                    items.Add(itemT);
                }
            }
        }

        public int ItemCount(int itemId)
        {
            return GetItem(itemId)?.Num ?? 0;
        }

        public void TraverseItems(Action<ItemBase, int> fn)
        {
            // dictItems.Keys.ToList() 用来避免出现 InvalidOperationException: Collection was modified
            // todo: magic number
            var list = ListPool<int>.Claim(8);
            list.AddRange(dictItems.Keys);
            foreach (int itemId in list)
            {
                var item = dictItems[itemId];
                fn(item, itemId);
            }
            ListPool<int>.Release(list);
        }

        public void TraverseItemsQuitable(Func<ItemBase, int, bool> fn)
        {
            // dictItems.Keys.ToList() 用来避免出现 InvalidOperationException: Collection was modified
            // todo: magic number
            var list = ListPool<int>.Claim(8);
            list.AddRange(dictItems.Keys);
            foreach (int itemId in list)
            {
                var item = dictItems[itemId];
                bool quit = fn(item, itemId);
                if (quit)
                {
                    break;
                }
            }
            ListPool<int>.Release(list);
        }

        public void AddItem(ItemBase item)
        {
            dictItems.Add(item.ItemId, item);
        }

        /// <inheritdoc />
        public override void OnWillInject(DiContainer container)
        {
            foreach (var item in dictItems.Values)
            {
                item.OnWillInject(container);
            }
        }

        /// <inheritdoc />
        public override void OnLoaded()
        {
            var items = ListPool<ItemBase>.Claim(dictItems.Count);
            items.AddRange(dictItems.Values);

            foreach (var item in items)
            {
                item.OnWillLoad();
            }

            // self check
            int i = 0;
            while (i < items.Count)
            {
                var item = items[i];

                if (item.SelfCheck())
                {
                    i++;
                    continue;
                }

                dictItems.Remove(item.ItemId);
                items.FastRemoveAt(i);
                item.OnRemoved();
            }

            foreach (var item in items)
            {
                item.OnLoaded();
            }


            items.ReleaseToPool();
            signalBus.Fire<OnInventoryLoadedSignal>();
        }

        /// <inheritdoc />
        public override void OnWillSave()
        {
            foreach (var item in dictItems.Values)
            {
                item.OnWillSave();
            }
        }

        #endregion
        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion
    }
}