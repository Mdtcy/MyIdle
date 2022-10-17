/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-01-02 18:14:26
 * @modify date 2019-01-02 18:14:26
 * @desc [方便编辑UIMultiImageButtonSlave状态]
 */

#pragma warning disable 0649

using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

namespace HM.UIRichButton
{
	/// <summary>
	/// 方便编辑UIMultiImageButtonSlave状态
	/// </summary>
	public class UIRichButtonMasterEditor : MonoBehaviour
	{
		#region FIELDS
		// master组件
		[SerializeField]
		private UIRichButtonMaster master;
		#endregion

		#region PROPERTIES
		#endregion

		#region PUBLIC METHODS
		#endregion

		#region PROTECTED METHODS
		#endregion

		#region PRIVATE METHODS

#if UNITY_EDITOR
		[Button("刷新slave列表")]
		private void RefreshSlaves()
		{
			master.ForceRefreshSlaves();
		}

		[Button("切换为Normal状态")]
		private void ChangeToNormalState() { ChangeAllSlavesToState("Normal"); }

		[Button("切换为Pressed状态")]
		private void ChangeToPressedState() { ChangeAllSlavesToState("Pressed"); }

		[Button("切换为Highlighted状态")]
		private void ChangeToHighlightedState() { ChangeAllSlavesToState("Highlighted"); }

		[Button("切换为Disabled状态")]
		private void ChangeToDisabledState() { ChangeAllSlavesToState("Disabled"); }

		// 切换slaves为指定状态
		private void ChangeAllSlavesToState(string state)
		{
			master.ForceRefreshSlaves();
			master.ChangeSlavesToState(state);
		}
#endif
		#endregion

		#region STATIC METHODS
		#endregion
	}
}

#pragma warning restore 0649
