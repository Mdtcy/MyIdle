#pragma warning disable 0649

using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HM.UI
{
	/// <summary>
	/// 根据toggle状态切换对象显示
	/// </summary>
    public class UIToggleSwitch : MonoBehaviour
    {
        #region FIELDS

        // 选中时显示
        [LabelText("选中时显示")]
        [SerializeField]
        private GameObject goOn;

        // 未选中时显示
        [LabelText("未选中时显示")]
        [SerializeField]
        private GameObject goOff;

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

        // 状态变化时
        private void OnToggleChanged(bool isOn)
        {
	        if (goOn != null)
	        {
		        goOn.SetActive(isOn);
	        }

	        if (goOff != null)
	        {
		        goOff.SetActive(!isOn);
	        }
        }

        // 延迟一帧初始化状态
        private IEnumerator<float> IToggleOnce()
        {
	        yield return Timing.WaitForOneFrame;
	        OnToggleChanged(toggle.isOn);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}

#pragma warning restore 0649