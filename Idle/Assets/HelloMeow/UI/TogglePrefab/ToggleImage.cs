/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-10-11 17:10:43
 * @modify date 2019-10-11 17:10:43
 * @desc [description]
 */

#pragma warning disable 0649

using UnityEngine;
using UnityEngine.UI;

namespace HM.UI.Toggle
{
    public class ToggleImage : ToggleBase
    {
        #region FIELDS

        [SerializeField]
        private Image targetImage;

        [SerializeField]
        private Sprite spOn;

        [SerializeField]
        private Sprite spOff;

        [SerializeField]
        private bool autoFit = true;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        protected override void OnToggleChanged()
        {
            base.OnToggleChanged();
            targetImage.sprite = isOn ? spOn : spOff;
            if (autoFit)
            {
                targetImage.SetNativeSize();
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
