/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-10-06 08:10:40
 * @modify date 2019-10-06 08:10:40
 * @desc [description]
 */

#pragma warning disable 0649

using Sirenix.OdinInspector;
using UnityEngine;

namespace HM.UI.Toggle
{
    public class TogglePrefab : ToggleBase
    {
        #region FIELDS

        [BoxGroup("Transition")]
        [SerializeField]
        [LabelText("On Prefab")]
        private GameObject pfbOn;

        [BoxGroup("Transition")]
        [SerializeField]
        [LabelText("Off Prefab")]
        private GameObject pfbOff;

		#endregion

		#region PROPERTIES

		#endregion

		#region PUBLIC METHODS
        #endregion

        #region PROTECTED METHODS

        protected override void OnToggleChanged()
        {
	        base.OnToggleChanged();
	        if (pfbOn != null) pfbOn.SetActive(isOn);
            if (pfbOff != null) pfbOff.SetActive(!isOn);
        }

        #endregion

        #region PRIVATE METHODS

		#endregion

		#region STATIC METHODS

		#endregion
    }
}

#pragma warning restore 0649
