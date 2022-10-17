/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-01-05 10:01:51
 * @modify date 2019-01-05 10:01:51
 * @desc [多样式按钮slave - 文本]
 */

#pragma warning disable 0649

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace HM.UIRichButton
{
    [Serializable]
    public class UIRichButtonSlaveGraphic : UIRichButtonSlave
    {
        #region FIELDS

        // 文本组件
        [Required]
        [SerializeField]
        private Graphic txt;

        // ------- Normal --------
        
        // Normal状态下，是否自定义位置
        [TabGroup("Normal")]
        [LabelText("CustomPos")]
        [SerializeField]
        private bool customNormalPos;
        
        // Normal状态下，所属transform的localPosition
        [TabGroup("Normal")]
        [LabelText("Position")]
        [HideIf("customNormalPos")]
        [SerializeField]
        private Vector2 posNormal;

        // Normal状态下，文本的颜色值
        [TabGroup("Normal")]
        [LabelText("Color")]
        [OnValueChanged("PreviewColorNormal")]
        [SerializeField]
        private Color colorNormal = Color.white;

        // ------- Pressed --------
        
        // Pressed状态下，是否自定义位置
        [TabGroup("Pressed")]
        [LabelText("CustomPos")]
        [SerializeField]
        private bool customPressPos;
        
        // Pressed状态下，所属transform的localPosition
        [TabGroup("Pressed")]
        [LabelText("Position")]
        [HideIf("customPressPos")]
        [SerializeField]
        private Vector2 posPressed;

        // Pressed状态下，文本的颜色值
        [TabGroup("Pressed")]
        [LabelText("Color")]
        [OnValueChanged("PreviewColorPressed")]
        [SerializeField]
        private Color colorPressed = Color.white;

        // ------- Highlighted --------
        
        // Pressed状态下，是否自定义位置
        [TabGroup("Highlighted")]
        [LabelText("CustomPos")]
        [HideIf("sameAsNormal")]
        [SerializeField]
        private bool customHighlightedPos;
        
        // Highlighted状态下，所属transform的localPosition
        [HideInInspector]
        [TabGroup("Highlighted")]
        [HideIf("customHighlightedPos")]
        [LabelText("Position")]
        [HideIf("sameAsNormal")]
        [SerializeField]
        private Vector2 posHighlighted;

        // Highlighted状态下，文本的颜色值
        [TabGroup("Highlighted")]
        [LabelText("Color")]
        [OnValueChanged("PreviewColorHighlighted")]
        [HideIf("sameAsNormal")]
        [SerializeField]
        private Color colorHighlighted = Color.white;

        // 如果为true，则使用Normal状态的设置
        [TabGroup("Highlighted")]
        [SerializeField]
        private bool sameAsNormal = true;

        // ------- Disabled --------
        // Pressed状态下，是否自定义位置
        [TabGroup("Disabled")]
        [LabelText("CustomPos")]
        [HideIf("sameAsPressed")]
        [SerializeField]
        private bool customDisabledPos;

        // Disabled状态下，所属transform的localPosition
        [TabGroup("Disabled")]
        [LabelText("Position")]
        [HideIf("customDisabledPos")]
        [HideIf("sameAsPressed")]
        [SerializeField]
        private Vector2 posDisabled;

        // Disabled状态下，文本的颜色值
        [TabGroup("Disabled")]
        [LabelText("Color")]
        [OnValueChanged("PreviewColorDisabled")]
        [HideIf("sameAsPressed")]
        [SerializeField]
        private Color colorDisabled = Color.white;

        // 如果为true，则使用Pressed状态的设置
        [TabGroup("Disabled")]
        [SerializeField]
        private bool sameAsPressed = true;

        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS
        #endregion

        #region PROTECTED METHODS

        // 切换到Normal状态
        protected override void ChangeToNormal()
        {
            if (!customNormalPos)
            {
                transform.localPosition = posNormal;
            }
            txt.color = colorNormal;
        }

        // 切换到Pressed状态
        protected override void ChangeToPressed()
        {
            if (!customPressPos)
            {
                transform.localPosition = posPressed;
            }
            txt.color = colorPressed;
        }

        // 切换到Highlighted状态
        protected override void ChangeToHighlighted()
        {
            if (sameAsNormal)
            {
                ChangeToNormal();
            }
            else
            {
                if (!customHighlightedPos)
                {
                    transform.localPosition = posHighlighted;
                }
                txt.color = colorHighlighted;
            }
        }
        // 切换到Disabled状态
        protected override void ChangeToDisabled()
        {
            if (sameAsPressed)
            {
                ChangeToPressed();
            }
            else
            {
                if (!customDisabledPos)
                {
                    transform.localPosition = posDisabled;
                }
                txt.color = colorDisabled;
            }
        }
        #endregion

        #region PRIVATE METHODS
#if UNITY_EDITOR
        private void Reset()
        {
            txt = GetComponent<Graphic>();
        }

        [TabGroup("Normal")]
        [Button("Use Current Position")]
        private void SetPositionNormal() { posNormal = transform.localPosition; }

        [TabGroup("Pressed")]
        [Button("Use Current Position")]
        private void SetPositionPressed() { posPressed = transform.localPosition; }

        [TabGroup("Highlighted")]
        [HideIf("sameAsNormal")]
        [Button("Use Current Position")]
        private void SetPositionHighlighted() { posHighlighted = transform.localPosition; }

        [TabGroup("Disabled")]
        [HideIf("sameAsPressed")]
        [Button("Use Current Position")]
        private void SetPositionDisabled() { posDisabled = transform.localPosition; }

        [TabGroup("Normal")]
        [Button("Set X")]
        private void SetPositionNormalX() { posNormal.x = transform.localPosition.x; }

        [TabGroup("Pressed")]
        [Button("Set X")]
        private void SetPositionPressedX() { posPressed.x = transform.localPosition.x; }

        [TabGroup("Highlighted")]
        [HideIf("sameAsNormal")]
        [Button("Set X")]
        private void SetPositionHighlightedX() { posHighlighted.x = transform.localPosition.x; }

        [TabGroup("Disabled")]
        [HideIf("sameAsPressed")]
        [Button("Set X")]
        private void SetPositionDisabledX() { posDisabled.x = transform.localPosition.x; }

        private void PreviewColorNormal() { txt.color = colorNormal; }
        private void PreviewColorPressed() { txt.color = colorPressed; }
        private void PreviewColorHighlighted() { txt.color = colorHighlighted; }
        private void PreviewColorDisabled() { txt.color = colorDisabled; }
#endif
        #endregion

        #region STATIC METHODS
        #endregion
    }
}

#pragma warning restore 0649
