/**
 * @author [Boluo]
 * @email [tktetb@163.com]
 * @create date 2022年9月26日 18:20:01
 * @modify date 2022年9月26日 18:20:01
 * @desc [简易的显示Dialog，只能设置类型和ShowModalOption]
 */

using HM.GameBase;
using NewLife.Defined;
using NewLife.Defined.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.UI.DialogLauncher
{
    /// <summary>
    /// 简易的显示Dialog，只能设置类型和ShowModalOption
    /// </summary>
    public class ShowDialog : MonoBehaviour
    {
        #region FIELDS

        // Dialog类型
        [SerializeField]
        private UiDialogType dialogType;

        [SerializeField]
        private UiManager.ShowModalOption showModalOption;

        // inject
        private IUIDialogLauncher uiDialogLauncher;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(IUIDialogLauncher uiDialogLauncher)
        {
            this.uiDialogLauncher = uiDialogLauncher;
        }

        /// <summary>
        /// 显示UI
        /// </summary>
        [Button]
        public void Show()
        {
            uiDialogLauncher.Prepare(dialogType)
                            .SetShowOption(showModalOption)
                            .Launch();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649