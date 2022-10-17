/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-10-14 14:03:23
 * @modify date 2022-10-14 14:03:23
 * @desc []
 */

using NewLife.Defined.CustomAttributes;
using NewLife.Defined.Interfaces;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.Defined
{
    public class LaunchUiDialogAtInspector : MonoBehaviour
    {
        #region FIELDS

        [UiDialogType]
        [SerializeField]
        private string uiDialogType;

        // * injected
        private IUIDialogLauncher launcher;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(IUIDialogLauncher launcher)
        {
            this.launcher = launcher;
        }

        /// <summary>
        /// 显示指定uiDialog
        /// </summary>
        public void Launch()
        {
            launcher.Launch(uiDialogType);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC

        #endregion


    }
}
#pragma warning restore 0649