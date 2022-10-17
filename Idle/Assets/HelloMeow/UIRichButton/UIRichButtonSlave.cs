/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-01-05 10:01:51
 * @modify date 2019-01-05 10:01:51
 * @desc [多样式按钮slave基类]
 */
using System;
using UnityEngine;

namespace HM.UIRichButton
{
	[Serializable]
	public class UIRichButtonSlave : MonoBehaviour
	{
		#region FIELDS
		#endregion

		#region PROPERTIES
		#endregion

		#region PUBLIC METHODS
		/// <summary>
		/// 接收外部发来的状态信息，更换上对应的图片
		/// </summary>
		/// <param name="state">Normal/Pressed/Highlighted/Disabled四种</param>
		public virtual void ChangeToState(string state)
		{
			switch (state)
			{
				case "Normal":
				default:
					ChangeToNormal();
					break;
				case "Pressed":
					ChangeToPressed();
					break;
				case "Highlighted":
					ChangeToHighlighted();
					break;
				case "Disabled":
					ChangeToDisabled();
					break;
			}
		}
		#endregion

		#region PROTECTED METHODS
		// 切换为Normal状态
		protected virtual void ChangeToNormal() {}
		// 切换为Pressed状态
		protected virtual void ChangeToPressed() {}
		// 切换为Highlighted状态
		protected virtual void ChangeToHighlighted() {}
		// 切换为Disabled状态
		protected virtual void ChangeToDisabled() {}
		#endregion

		#region PRIVATE METHODS
		#endregion

		#region STATIC METHODS
		#endregion
	}
}