using System.Collections.Generic;
using HM.Notification;
using UnityEngine;

namespace HM.GameBase
{
    public partial class MonoBehaviourEx : MonoBehaviour
    {
        #region FIELDS
        private Dictionary<int, NotificationCenter.OnNotification> _notifications;
        private Dictionary<int, NotificationCenter.OnNotification> _lifeNotifications;

        #endregion

        #region PROPERTIES
        private Dictionary<int, NotificationCenter.OnNotification> Notifications => _notifications ?? (_notifications = new Dictionary<int, NotificationCenter.OnNotification>());
        private Dictionary<int, NotificationCenter.OnNotification> LifeNotifications => _lifeNotifications ?? (_lifeNotifications = new Dictionary<int, NotificationCenter.OnNotification>());


        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS
        protected void RegisterNotification(int key, NotificationCenter.OnNotification callback = null)
        {
            RemoveNotification(key);
            callback = callback ?? OnNotificationArrived;
            Notifications.Add(key, callback);
            NotificationCenter.Instance.Subscribe(key, callback);
        }

        protected virtual void OnNotificationArrived(Notification.Notification note)
        {
            HMLog.LogVerbose($"[{name}]收到事件通知[key = {note.Key}]");
        }

        protected void RemoveNotification(int key)
        {
            if (Notifications.ContainsKey(key))
            {
                NotificationCenter.Instance.Unsubscribe(key, Notifications[key]);
                Notifications.Remove(key);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_notifications != null)
            {
                foreach (var key in _notifications.Keys)
                {
                    NotificationCenter.Instance.Unsubscribe(key, _notifications[key]);
                }
                _notifications.Clear();
            }

            if (_lifeNotifications != null)
            {
                foreach (var key in _lifeNotifications.Keys)
                {
                    NotificationCenter.Instance.Unsubscribe(key, _lifeNotifications[key]);
                }
                _lifeNotifications.Clear();
            }
        }

        protected virtual void OnEnable()
        {
            if (_notifications != null)
            {
                foreach (var key in _notifications.Keys)
                {
                    NotificationCenter.Instance.Subscribe(key, _notifications[key]);
                }
            }
        }

        protected virtual void OnDisable()
        {
            if (_notifications != null)
            {
                foreach (var key in _notifications.Keys)
                {
                    NotificationCenter.Instance.Unsubscribe(key, _notifications[key]);
                }
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void RemoveLifeTimeNotification(int key)
        {
            if (LifeNotifications.ContainsKey(key))
            {
                NotificationCenter.Instance.Unsubscribe(key, LifeNotifications[key]);
                LifeNotifications.Remove(key);
            }
        }
        #endregion

        #region STATIC METHODS
        #endregion
    }
}