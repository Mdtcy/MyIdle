/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-19 23:06:26
 * @modify date 2020-06-19 23:06:26
 * @desc [description]
 */
#pragma warning disable 0649
using System;
using System.Reflection;
using HM.Notification;
using NewLife.Defined;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace NewLife.Support.Notification
{
    public class UIReceiveNotification : MonoBehaviour
    {
	    [Serializable]
        // UnityEvent callback for when a toggle is toggled.
        public class OnNotificationEvent : UnityEvent<HM.Notification.Notification> {}

        [SerializeField]
        private OnNotificationEvent onNotificationEvent;

        [BoxGroup("事件Key"), ToggleLeft, LabelText("是否手动输入事件key")][SerializeField]
        private bool inputKey;

        [BoxGroup("事件Key"), LabelText("手动输入事件Key")]
        [SerializeField, EnableIf("inputKey")]
        private int notifyKeyByInput;

        [BoxGroup("事件Key"), ValueDropdown("AllNotifyKeys"), LabelText("选择系统定义的事件Key")]
        [SerializeField, DisableIf("inputKey")]
        private int notifyKeyBySystem;

        private IReceiveNotification notificationReceiver;

        [Inject]
        public void Construct(IReceiveNotification receiver)
        {
	        notificationReceiver = receiver;
        }

        private void Start()
        {
	        notificationReceiver.Subscribe(inputKey ? notifyKeyByInput : notifyKeyBySystem, OnNotification);
        }

        private void OnEnable()
        {
	        notificationReceiver.Subscribe(inputKey ? notifyKeyByInput : notifyKeyBySystem, OnNotification);
        }

        private void OnDisable()
        {
	        notificationReceiver.Unsubscribe(inputKey ? notifyKeyByInput : notifyKeyBySystem, OnNotification);
        }

        private void OnNotification(HM.Notification.Notification note)
        {
	        onNotificationEvent?.Invoke(note);
        }

#if UNITY_EDITOR
        private static readonly ValueDropdownList<int> AllNotifyKeys = new ValueDropdownList<int>{};

        [BoxGroup("事件Key")]
        [GUIColor(0, 1, 0)]
        [Button("扫描所有系统事件key", ButtonSizes.Medium)]
        private void ScanNotifyKeys()
        {
            AllNotifyKeys.Clear();
            foreach (var field in typeof(NotifyKeys).GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				var key = field.Name;
				var value = (int)field.GetValue(null);
				AllNotifyKeys.Add(key, value);
			}
            foreach (var field in typeof(NewLifeNotifyKeys).GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				var key = field.Name;
				var value = (int)field.GetValue(null);
				AllNotifyKeys.Add(key, value);
			}
            HM.HMLog.LogDebug("扫描所有NotifyKey成功");
        }
#endif
    }
}
#pragma warning restore 0649