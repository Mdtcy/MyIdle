/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-08 23:05:22
 * @modify date 2020-05-08 23:05:22
 * @desc [description]
 */

#pragma warning disable 0649

using UnityEngine;
using UnityEngine.Events;

namespace HM.UI.Toggle
{
    public class ToggleCallback : ToggleBase
    {
        #region FIELDS

        [SerializeField]
        private UnityEvent actionOn;

        [SerializeField]
        private UnityEvent actionOff;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        protected override void OnToggleChanged()
        {
            base.OnToggleChanged();

            if (isOn)
            {
                actionOn?.Invoke();
            }
            else
            {
                actionOff.Invoke();
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

#endregion
    }
}

#pragma warning restore 0649
