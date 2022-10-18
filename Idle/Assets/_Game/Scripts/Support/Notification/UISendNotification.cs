/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-07-02 16:46:49
 * @modify date 2019-07-02 16:46:49
 * @desc [实现发送特定通知消息的功能]
 */

#pragma warning disable 0649

using HM;
#if UNITY_EDITOR
using System.Reflection;
#endif
using HM.Notification;
using NewLife.Defined;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace NewLife.Support.Notification
{
    public class UISendNotification : MonoBehaviour
    {
        #region FIELDS

        [BoxGroup("事件Key"), ToggleLeft, LabelText("是否手动输入事件key")]
        [SerializeField]
        private bool inputKey;

        [BoxGroup("事件Key"), LabelText("手动输入事件Key")]
        [SerializeField, EnableIf("inputKey")]
        private int notifyKeyByInput;

        [BoxGroup("事件Key"), ValueDropdown("AllNotifyKeys"), LabelText("选择系统定义的事件Key")]
        [SerializeField, DisableIf("inputKey")]
        private int notifyKeyBySystem;

        [Inject]
        private ISendNotification sendNotification;

        #if UNITY_EDITOR
        private static readonly ValueDropdownList<int> AllNotifyKeys = new ValueDropdownList<int> { };

        [BoxGroup("事件Key")]
        [GUIColor(0, 1, 0)]
        [Button("扫描所有系统事件key", ButtonSizes.Medium)]
        private void ScanNotifyKeys()
        {
            AllNotifyKeys.Clear();

            foreach (var field in typeof(NotifyKeys).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var key   = field.Name;
                var value = (int) field.GetValue(null);
                AllNotifyKeys.Add(key, value);
            }

            foreach (var field in typeof(NewLifeNotifyKeys).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var key   = field.Name;
                var value = (int) field.GetValue(null);
                AllNotifyKeys.Add(key, value);
            }

            HM.HMLog.LogDebug("扫描所有NotifyKey成功");
        }
        #endif

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        // ---------- no parameter ----------

        /// <summary>
        /// 发送无参消息
        /// </summary>
        [BoxGroup("发送无参消息")]
        [Button(ButtonSizes.Medium)]
        public void SendNoParameter()
        {
            if (GetNotifyKey(out int key))
            {
                HM.HMLog.LogVerbose($"[UISendNotification] 发送事件: {key}");
                #if UNITY_EDITOR
                sendNotification.Publish(key, null, v => { OnNotificationFinish(key, v); });
                #else
                sendNotification.Publish(key);
                #endif
            }
        }

        // ---------- int ----------

        [BoxGroup("发送int参数消息")]
        [SerializeField]
        private int intValue;

        /// <summary>
        /// 发送int参数消息
        /// </summary>
        [BoxGroup("发送int参数消息")]
        [Button(ButtonSizes.Medium)]
        public void SendInt()
        {
            if (GetNotifyKey(out int key))
            {
                HM.HMLog.LogVerbose($"[UISendNotification] 发送事件: {key} - {intValue}");
                #if UNITY_EDITOR
                sendNotification.Publish(key, intValue, v => OnNotificationFinish(key, v));
                #else
                sendNotification.Publish(key, intValue);
                #endif
            }
        }

        // ---------- UnityEngine.Object ----------
        [BoxGroup("发送string参数消息")]
        [SerializeField]
        private string someString;

        /// <summary>
        /// 发送Object参数消息
        /// </summary>
        [BoxGroup("发送string参数消息")]
        [Button(ButtonSizes.Medium)]
        public void SendString()
        {
            if (GetNotifyKey(out int key))
            {
                HM.HMLog.LogVerbose($"[UISendNotification] 发送事件: {key} - {someString}");
                #if UNITY_EDITOR
                sendNotification.Publish(key, someString, v => OnNotificationFinish(key, v));
                #else
                sendNotification.Publish(key, someString);
                #endif
            }
        }

        // ---------- UnityEngine.Object ----------
        [BoxGroup("发送Object参数消息")]
        [SerializeField]
        private Object someObject;

        /// <summary>
        /// 发送Object参数消息
        /// </summary>
        [BoxGroup("发送Object参数消息")]
        [Button(ButtonSizes.Medium)]
        public void SendObject()
        {
            if (GetNotifyKey(out int key))
            {
                HMLog.LogVerbose($"[UISendNotification] 发送事件: {key} - {someObject}");
                #if UNITY_EDITOR
                sendNotification.Publish(key, someObject, v => OnNotificationFinish(key, v));
                #else
                sendNotification.Publish(key, someObject);
                #endif
            }
        }

        // 刷新事件key
        private bool GetNotifyKey(out int key)
        {
            key = inputKey ? notifyKeyByInput : notifyKeyBySystem;

            return true;
        }

        // 回调
        private void OnNotificationFinish(int key, bool succeed)
        {
            HMLog.LogDebug($"[UISendNotification]EditorOnly::收到事件回调: key = {key}, succeed = {succeed}");
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

#pragma warning restore 0649