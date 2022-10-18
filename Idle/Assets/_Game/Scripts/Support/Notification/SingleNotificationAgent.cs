/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-03-24 13:32:44
 * @modify date 2022-03-24 13:32:44
 * @desc [只能监听一个消息]
 */

using HM;
using HM.Notification;
using UnityEngine;
using Zenject;

namespace NewLife.Support.Notification
{
    public class SingleNotificationAgent : MonoBehaviour
    {
        #region FIELDS

        private IReceiveNotification notificationReceiver;

        private int eventKey;

        private NotificationCenter.OnNotification callback;

        #endregion

        #region PROPERTIES

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
            if (this.eventKey != 0)
            {
                HMLog.LogWarning($"[SingleNotificationAgent][{name}]Already listening to event[{this.eventKey}], you should deregister first.");
                return;
            }

            this.eventKey = eventKey;
            callback      = onNotificationArrived;
            notificationReceiver.Subscribe(eventKey, onNotificationArrived);
        }

        /// <summary>
        /// 丢弃之前绑定的事件
        /// </summary>
        public void Dispose()
        {
            Deregister();
            eventKey = 0;
            callback = null;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnEnable()
        {
            if (eventKey != 0 && callback != null)
            {
                notificationReceiver.Subscribe(eventKey, callback);
            }
        }

        private void OnDisable()
        {
            Deregister();
        }

        private void OnDestroy()
        {
            Deregister();
        }

        private void Deregister()
        {
            if (eventKey != 0 && callback != null)
            {
                notificationReceiver.Unsubscribe(eventKey, callback);
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}