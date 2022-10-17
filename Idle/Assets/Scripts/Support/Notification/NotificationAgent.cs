/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-11 16:06:10
 * @modify date 2020-06-11 16:06:10
 * @desc [跟随GameObject生命周期，对事件进行监听]
 */

using System.Collections.Generic;
using HM.Notification;
using UnityEngine;
using Zenject;

namespace NewLife.Support.Notification
{
    public class NotificationAgent : MonoBehaviour
    {
        #region FIELDS

        private IReceiveNotification notificationReceiver;

        private Dictionary<int, NotificationCenter.OnNotification> listeners;

        #endregion

        #region PROPERTIES

        private Dictionary<int, NotificationCenter.OnNotification> Listeners =>
            listeners ?? (listeners = new Dictionary<int, NotificationCenter.OnNotification>());

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(IReceiveNotification receiver)
        {
            notificationReceiver = receiver;
        }

        /// <summary>
        /// 监听指定事件
        /// </summary>
        /// <param name="eventKey"></param>
        /// <param name="onNotificationArrived"></param>
        public void RegisterNotification(int eventKey, NotificationCenter.OnNotification onNotificationArrived)
        {
            if (Listeners.ContainsKey(eventKey))
            {
                Listeners.Remove(eventKey);
            }
            Listeners.Add(eventKey, onNotificationArrived);
            notificationReceiver.Subscribe(eventKey, onNotificationArrived);
        }

        /// <summary>
        /// 取消监听指定事件
        /// </summary>
        /// <param name="eventKey"></param>
        public void UnregisterNotification(int eventKey)
        {
            if (Listeners.ContainsKey(eventKey))
            {
                notificationReceiver.Unsubscribe(eventKey, Listeners[eventKey]);
                Listeners.Remove(eventKey);
            }
        }

        public void Pause()
        {
            if (listeners != null)
            {
                foreach (int key in listeners.Keys)
                {
                    notificationReceiver.Unsubscribe(key, listeners[key]);
                }
            }
        }

        public void Resume()
        {
            if (listeners != null)
            {
                foreach (int key in listeners.Keys)
                {
                    notificationReceiver.Subscribe(key, listeners[key]);
                }
            }
        }

        #endregion

        #region PROTECTED METHODS

        protected virtual void OnEnable()
        {
            Resume();
        }

        protected virtual void OnDisable()
        {
            Pause();
        }

        #endregion

        #region PRIVATE METHODS

        private void OnDestroy()
        {
            Pause();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}