/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-10-05 13:34:57
 * @modify date 2022-10-05 13:34:57
 * @desc []
 */

using NewLife.Defined.CustomAttributes;
using NewLife.Defined.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.EditorOnly
{
    public class UiDialogDebugger : MonoBehaviour
    {
        #region FIELDS

        [UiDialogType]
        [SerializeField]
        private string dialogType;

        [Inject]
        private IUIDialogLauncher launcher;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        [Button]
        private void Launch()
        {
            launcher.Launch(dialogType);
        }

        [Button]
        private void Hide()
        {
            launcher.Hide(dialogType);
        }

        #endregion

        #region STATIC

        #endregion


    }
}
#pragma warning restore 0649