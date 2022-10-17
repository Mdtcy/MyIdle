/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-08-02 17:08:36
 * @modify date 2021-08-02 17:08:36
 * @desc [批量获取指定类型的ItemBase，并且时刻保持集合是最新的]
 */

using System;
using System.Collections.Generic;
using HM.Extensions;
using HM.Notification;
using Zenject;

namespace HM.GameBase
{
    public class ItemsGetter<T> : IItemsGetter<T> where T : ItemBase
    {
        // 保存指定类型的所有item
        private List<T> items;

        // 当集合元素有增减时，向外发送通知
        private Action onCollectionChanged;

        private readonly IItemListGetter itemListGetter;

        public ItemsGetter()
        {
        }

        [Inject]
        public ItemsGetter(IReceiveNotification receiveNotification, IItemListGetter itemListGetter)
        {
            HMLog.LogVerbose($"ItemsGetter<{typeof(T).Name}> is created");

            // 目前只涉及有item增加的情况，删除情况没有考虑
            receiveNotification.Subscribe(NotifyKeys.kOnItemPicked, OnNotificationArrived);
            receiveNotification.Subscribe(NotifyKeys.kOnItemRemoved, OnNotificationArrived);
            this.itemListGetter = itemListGetter;
        }

        private void OnNotificationArrived(Notification.Notification notification)
        {
            if (notification.Key == NotifyKeys.kOnItemPicked)
            {
                if (notification.Data is T item)
                {
                    if (Get().AddNoDuplicate(item))
                    {
                        onCollectionChanged?.Invoke();
                    }
                }
            }
            else if (notification.Key == NotifyKeys.kOnItemRemoved)
            {
                if (notification.Data is T item)
                {
                    if (items != null && items.Remove(item))
                    {
                        onCollectionChanged?.Invoke();
                    }
                }
            }
        }

        /// <inheritdoc />
        public List<T> Get()
        {
            if (items == null)
            {
                Initialize();
            }

            return items;
        }

        /// <inheritdoc />
        public int Count()
        {
            return Get().Count;
        }

        /// <inheritdoc />
        public void Subscribe(Action onValueChanged)
        {
            onCollectionChanged += onValueChanged;
        }

        /// <inheritdoc />
        public void Unsubscribe(Action onValueChanged)
        {
            // ReSharper disable once DelegateSubtraction
            onCollectionChanged -= onValueChanged;
        }

        /// <inheritdoc />
        public void Filter(Func<T, bool> predicate, ref List<T> result)
        {
            if (predicate == null || result == null)
            {
                return;
            }

            result.Clear();

            foreach (var itemBase in Get())
            {
                if (predicate.Invoke(itemBase))
                {
                    result.Add(itemBase);
                }
            }
        }

        /// <inheritdoc />
        public bool Contains(int itemId)
        {
            foreach (var item in Get())
            {
                if (item.ItemId == itemId)
                {
                    return true;
                }
            }

            return false;
        }

        public void OnEnterPool()
        {
            items?.ReleaseToPool();
            items = null;
        }

        private void Initialize()
        {
            items = ListPool<T>.Claim(32);
            itemListGetter.GetItems(ref items);
        }
    }
}