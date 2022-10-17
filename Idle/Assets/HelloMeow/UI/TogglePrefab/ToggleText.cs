/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-04-16 13:04:34
 * @modify date 2020-04-16 13:04:34
 * @desc [description]
 */
#pragma warning disable 0649
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HM.UI.Toggle
{
    public class ToggleText : ToggleBase
    {
        [Serializable]
        public struct Style
        {
            // 文字颜色
            public Color textColor;
        }

        #region FIELDS

        [SerializeField]
        private Text text;

        [SerializeField]
        private Style styleOn;

        [SerializeField]
        private Style styleOff;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        protected override void OnToggleChanged()
        {
            base.OnToggleChanged();
            var style = isOn ? styleOn : styleOff;
            text.color = style.textColor;
        }
        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649