/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-01-05 10:01:51
 * @modify date 2019-01-05 10:01:51
 * @desc [多样式按钮slave - 图片]
 */

#pragma warning disable 0649

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace HM.UIRichButton
{
    [Serializable]
    public class UIRichButtonSlaveImage : UIRichButtonSlave
    {
        #region FIELDS

        // 图片组件
        [Required]
        [SerializeField]
        private Image img;

        [SerializeField]
        private bool fitImage;

        // ------- Normal --------
        // Normal状态下，所属transform的localPosition
        [TabGroup("Normal")]
        [LabelText("Position")]
        [SerializeField]
        private Vector2 posNormal;

        // Normal状态下ImageTransition的设置
        [TabGroup("Normal")]
        [EnumToggleButtons]
        [LabelText("Transition Type")]
        public ImageTransition itNormal;

        // Normal状态下ImageTransition设置如果为Sprite，会用_spNormal设置_img
        [TabGroup("Normal")]
        [LabelText("Sprite")]
        [EnableIf("itNormal", ImageTransition.Sprite)]
        [SerializeField]
        private Sprite spNormal;

        // Normal状态下ImageTransition设置如果为Material，会用_mtNormal设置_img的材质
        [TabGroup("Normal")]
        [LabelText("Material")]
        [EnableIf("itNormal", ImageTransition.Material)]
        [SerializeField]
        private Material mtNormal;

        // ------- Pressed --------

        // Pressed状态下，所属transform的localPosition
        [TabGroup("Pressed")]
        [LabelText("Position")]
        [SerializeField]
        private Vector2 posPressed;

        // Pressed状态下ImageTransition的设置
        [TabGroup("Pressed")]
        [EnumToggleButtons]
        [LabelText("Transition Type")]
        public ImageTransition itPressed;

        // Pressed状态下ImageTransition设置如果为Sprite，会用_spPressed设置_img
        [TabGroup("Pressed")]
        [LabelText("Sprite")]
        [EnableIf("itPressed", ImageTransition.Sprite)]
        [SerializeField]
        private Sprite spPressed;

        // Pressed状态下ImageTransition设置如果为Material，会用_mtPressed设置_img的材质
        [TabGroup("Pressed")]
        [LabelText("Material")]
        [EnableIf("itPressed", ImageTransition.Material)]
        [SerializeField]
        private Material mtPressed;

        // ------- Highlighted --------

        // Highlighted状态下，所属transform的localPosition
        [TabGroup("Highlighted")]
        [LabelText("Position")]
        [HideIf("sameAsNormal")]
        [SerializeField]
        private Vector2 posHighlighted;

        // Highlighted状态下ImageTransition的设置
        [HideIf("sameAsNormal")]
        [TabGroup("Highlighted")]
        [EnumToggleButtons]
        [LabelText("Transition Type")]
        public ImageTransition itHighlighted;

        // Highlighted状态下ImageTransition设置如果为Sprite，会用_spHighlighted设置_img
        [HideIf("sameAsNormal")]
        [TabGroup("Highlighted")]
        [LabelText("Sprite")]
        [EnableIf("itHighlighted", ImageTransition.Sprite)]
        [SerializeField]
        private Sprite spHighlighted;

        // Highlighted状态下ImageTransition设置如果为Material，会用_mtHighlighted设置_img的材质
        [HideIf("sameAsNormal")]
        [TabGroup("Highlighted")]
        [LabelText("Material")]
        [EnableIf("itHighlighted", ImageTransition.Material)]
        [SerializeField]
        private Material mtHighlighted;

        // 如果为true，则使用Normal状态的设置
        [TabGroup("Highlighted")]
        [SerializeField]
        private bool sameAsNormal = true;

        // ------- Disabled --------

        // Disabled状态下，所属transform的localPosition
        [TabGroup("Disabled")]
        [LabelText("Position")]
        [HideIf("sameAsPressed")]
        [SerializeField]
        private Vector2 posDisabled;

        // Disabled状态下ImageTransition的设置
        [HideIf("sameAsNormal")]
        [TabGroup("Disabled")]
        [EnumToggleButtons]
        [LabelText("Transition Type")]
        public ImageTransition itDisabled;

        // Disabled状态下ImageTransition设置如果为Sprite，会用_spDisabled设置_img
        [HideIf("sameAsNormal")]
        [TabGroup("Disabled")]
        [LabelText("Sprite")]
        [EnableIf("itDisabled", ImageTransition.Sprite)]
        [SerializeField]
        private Sprite spDisabled;

        // Disabled状态下ImageTransition设置如果为Material，会用_mtDisabled设置_img的材质
        [HideIf("sameAsNormal")]
        [TabGroup("Disabled")]
        [LabelText("Material")]
        [EnableIf("itDisabled", ImageTransition.Material)]
        [SerializeField]
        private Material mtDisabled;

        // 如果为true，则使用Pressed状态的设置
        [TabGroup("Disabled")]
        [SerializeField]
        private bool sameAsPressed = true;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 普通状态图片
        /// </summary>
        public Sprite NormalSprite
        {
            get { return spNormal; }
            set { spNormal = value; }
        }

        /// <summary>
        /// 按下状态图片
        /// </summary>
        public Sprite PressSprite
        {
            get
            {
                return spPressed;
            }
            set
            {
                spPressed = value;
            }
        }

        #endregion

        #region PUBLIC METHODS
        #endregion

        #region PROTECTED METHODS

        // 切换到Normal状态
        protected override void ChangeToNormal()
        {
            transform.localPosition = posNormal;
            if (itNormal == ImageTransition.Sprite)
            {
                img.sprite = spNormal;
                if (fitImage)
                {
                    img.SetNativeSize();
                }
            }
            else
            {
                img.material = mtNormal;
            }
        }
        // 切换到Pressed状态
        protected override void ChangeToPressed()
        {
            transform.localPosition = posPressed;
            if (itPressed == ImageTransition.Sprite)
            {
                img.sprite = spPressed;
                if (fitImage)
                {
                    img.SetNativeSize();
                }
            }
            else
            {
                img.material = mtPressed;
            }
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
                transform.localPosition = posHighlighted;
                if (itHighlighted == ImageTransition.Sprite)
                {
                    img.sprite = spHighlighted;
                    if (fitImage)
                    {
                        img.SetNativeSize();
                    }
                }
                else
                {
                    img.material = mtHighlighted;
                }
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
                transform.localPosition = posDisabled;
                if (itDisabled == ImageTransition.Sprite)
                {
                    img.sprite = spDisabled;
                    if (fitImage)
                    {
                        img.SetNativeSize();
                    }
                }
                else
                {
                    img.material = mtDisabled;
                }
            }
        }
        #endregion

        #region PRIVATE METHODS
#if UNITY_EDITOR
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
#endif
        #endregion

        #region STATIC METHODS
        #endregion
    }
}

#pragma warning restore 0649
