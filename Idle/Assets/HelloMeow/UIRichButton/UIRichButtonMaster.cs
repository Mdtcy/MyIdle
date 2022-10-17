/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-15 17:05:55
 * @modify date 2018-12-15 17:05:55
 * @desc [支持多图变换状态的按钮 - 发送状态信息给slave以更改按钮样式]
 */
using UnityEngine;
using UnityEngine.Events;

namespace HM.UIRichButton
{
    /// <summary>
    /// 需和UIRichButtonSlave配合使用，从而实现多个图片组件切换不同状态图片的按钮功能
    /// </summary>
    [System.Serializable]
    public class UIRichButtonMaster : UnityEngine.UI.Button
    {
        #region FIELDS
        // slave列表（自动获取），所有slave都必须是当前结点的子结点
        private UIRichButtonSlave[] slaveList;

        [SerializeField]
        private UnityEvent evtOnDisabled;

        [SerializeField]
        private UnityEvent evtOnHighlighted;

        [SerializeField]
        private UnityEvent evtOnPressed;

        [SerializeField]
        private UnityEvent evtOnSelected;

        [SerializeField]
        private UnityEvent evtOnNormal;

        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS
        // 强制刷新slave列表
        public void ForceRefreshSlaves()
        {
            slaveList = transform.GetComponentsInChildren<UIRichButtonSlave>();
        }

        // 通知slaves状态有更新
        public void ChangeSlavesToState(string state)
        {
            foreach (var btn in slaveList)
            {
                btn.ChangeToState(state);
            }
        }
        #endregion

        #region PROTECTED METHODS
        // 重写以监听按钮状态切换消息，发送给slave并且触发对应的事件
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (state == SelectionState.Disabled)
            {
                evtOnDisabled?.Invoke();
            }
            else if (state == SelectionState.Pressed)
            {
                evtOnPressed?.Invoke();
            }
            else if (state == SelectionState.Selected)
            {
                evtOnSelected?.Invoke();
            }
            else if (state == SelectionState.Highlighted)
            {
                evtOnHighlighted?.Invoke();
            }
            else if (state == SelectionState.Normal)
            {
                evtOnNormal?.Invoke();
            }

            if (slaveList == null || slaveList.Length <= 0)
            {
                ForceRefreshSlaves();
            }
            Debug.Assert(slaveList.Length > 0);
            ChangeSlavesToState(state.ToString());
        }

        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion
    }
}