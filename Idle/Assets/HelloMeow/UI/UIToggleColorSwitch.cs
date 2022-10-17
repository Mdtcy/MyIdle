/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2020-09-22 16:52:22
 * @desc [根据toggle状态切换颜色]
 */

#pragma warning disable 0649

using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace HM.UI
{
    /// <summary>
    /// 根据toggle状态切换颜色
    /// </summary>
    public class UIToggleColorSwitch : MonoBehaviour
    {
        #region FIELDS

        // 选中时显示的颜色
        [LabelText("选中时显示")]
        [SerializeField]
        private Color selected;

        // 未选中时显示的颜色
        [LabelText("未选中时显示")]
        [SerializeField]
        private Color unselected;

        // 目标显示对象
        [LabelText("Target Graphic")]
        [SerializeField]
        private Graphic target;

        // 目标toggle
        private UnityEngine.UI.Toggle toggle;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// 如果toggle.OnValueChanged放在Start里，初始isOn=false，并且gameObject或其父节点会反复activate/deactivate，那么在下一次active的时候，
        /// toggle会发送两个消息，一个isOn=true，一个isOn=false，为了避免这种不一致，在onEnable/onDisable中注册和解除注册toggle事件
        /// </summary>
        private void OnEnable()
        {
            toggle = toggle ? toggle : GetComponent<UnityEngine.UI.Toggle>();
            toggle.onValueChanged.AddListener(OnToggleChanged);
            // 必须在OnEnable里调用一次，否则父节点active之后tab页面不会刷新
            IToggleOnce().RunCoroutine();
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }

        // 状态变化
        private void OnToggleChanged(bool isOn)
        {
            target.color = isOn ? selected : unselected;
        }

        // 延迟一帧，等待toggle初始化完成
        private IEnumerator<float> IToggleOnce()
        {
            yield return Timing.WaitForOneFrame;
            OnToggleChanged(toggle.isOn);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC FIELDS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}

#pragma warning restore 0649