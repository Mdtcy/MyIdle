using System;
using System.Collections.Generic;
using HM.GameBase;

namespace HM.Notification
{
    public class Notification : IHMPooledObject
    {
        public int Key = 0;
        public object Data = null;
        public long LongValue = 0;
        public Action<bool> OnFinish = null; // optional callback

        public Notification() {}

        public void OnEnterPool()
        {
            Key = 0;
            Data = null;
            LongValue = 0;
            OnFinish = null;
        }

        public int IntValue => (int) LongValue;
    }

    /// <summary>
    /// 通知中心
    /// </summary>
    public class NotificationCenter : ISendNotification, IReceiveNotification
    {

        #region Publish

        public enum CallbackOccurrence
        {
            CE_ALLOW_MULTIPLE,
            CE_UNIQUE_ONLY,
        }

        private Notification Claim()
        {
            return ObjectPool<Notification>.Claim();
        }

        private void Release(Notification note)
        {
            ObjectPool<Notification>.Release(ref note);
        }

        private void Publish(Notification notification)
        {
            if (notification == null)return;
            // HMLog.LogVerbose($"[NotificationCenter]Publish {notification.Key} to "+
            //                $"{mDelegates[notification.Key].GetInvocationList().Length} subscribers");
            mDelegates[notification.Key](notification);
            Release(notification);
        }

        /// <summary>
        /// 发布指定消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Publish(int key, object data = null, Action<bool> onFinish = null)
        {
            if (!mDelegates.ContainsKey(key) || mDelegates[key] == null)
            {
                return;
            }
            var note = Claim();
            note.Key = key;
            note.Data = data;
            note.OnFinish = onFinish;
            Publish(note);
        }

        /// <summary>
        /// 发布指定消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Publish(int key, long data, Action<bool> onFinish = null)
        {
            if (!mDelegates.ContainsKey(key) || mDelegates[key] == null)
            {
                return;
            }
            var note = Claim();
            note.Key = key;
            note.LongValue = data;
            note.OnFinish = onFinish;
            Publish(note);
        }

        #endregion

        #region Subscription
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback">如果是lambda表达式，调用者必须要保存其引用，以正常取消订阅</param>
        /// <param name="callbackNumber"></param>
        public void Subscribe(int key, OnNotification callback,
            CallbackOccurrence callbackNumber = CallbackOccurrence.CE_UNIQUE_ONLY)
        {
            if (callback == null)return;

            if (callbackNumber == CallbackOccurrence.CE_UNIQUE_ONLY)
            {
                Unsubscribe(key, callback);
            }

            if (!mDelegates.ContainsKey(key))
            {
                mDelegates[key] = callback;
            }
            else
            {
                mDelegates[key] = mDelegates[key] + callback;
            }

            // HMLog.LogVerbose($"[NotificationCenter]Subscribe {key}, now we have {mDelegates[key].GetInvocationList().Length} subscribers");

        }

        /// <summary>
        /// 取消订阅某个事件，注意callback必须是单一函数，否则可能无效，比如(a,b,c) - (a,c) = (a,b,c)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        public void Unsubscribe(int key, OnNotification callback)
        {
            if (callback == null)return;
            if (!mDelegates.ContainsKey(key))return;

            // ReSharper disable once DelegateSubtraction
            mDelegates[key] = mDelegates[key] - callback;

            // HMLog.LogVerbose($"[NotificationCenter]Subscribe {key}, now we have {mDelegates[key].GetInvocationList().Length} subscribers");
        }

        /// <summary>
        /// 清空所有通知
        /// </summary>
        public void Clear()
        {
            mDelegates.Clear();
        }

        #endregion

        #region Instance

        private static NotificationCenter instance;

        public delegate void OnNotification(Notification notification);
        private NotificationCenter()
        {
            // todo: 测试一下最多同时需要多少通知
            mDelegates = new Dictionary<int, OnNotification>(50);
        }

        public static NotificationCenter Instance => instance ?? (instance = new NotificationCenter());

        #endregion

        private readonly Dictionary<int, OnNotification> mDelegates;
    }
}