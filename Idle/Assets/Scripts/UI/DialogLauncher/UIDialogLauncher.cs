/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-30 10:49:16
 * @modify date 2022-08-30 10:49:16
 * @desc [负责打开指定UiDialog]
 */

using HM;
using NewLife.Config;
using NewLife.Defined;
using NewLife.Defined.Interfaces;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.UI.DialogLauncher
{
    public class UIDialogLauncher : MonoBehaviour, IUIDialogLauncher
    {
        #region FIELDS

        [SerializeField]
        private UiDialogMappings mappings;

        // * local

        // 保证至少不报错
        private IUIDialogLaunchSetting     nullDlgSetting;
        private IUIMessageBoxLaunchSetting nullMsgSetting;

        // * injected
        private UIDialogLaunchSetting.Factory     dlgSettingFactory;

        private SignalBus signalBus;

        #endregion

        #region PROPERTIES

        private IUIDialogLaunchSetting NullDlgSetting =>
            nullDlgSetting ??= dlgSettingFactory.Create(UiDialogType.NotSpecified, null);

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(UIDialogLaunchSetting.Factory     dlgSettingFactory,
                              SignalBus signalBus)
        {
            this.dlgSettingFactory = dlgSettingFactory;
            this.signalBus         = signalBus;
        }

        /// <inheritdoc />
        public void Launch(UiDialogType dialogType)
        {
            Prepare(dialogType).Launch();
        }

        /// <inheritdoc />
        public void Launch(string dialogType)
        {
            Launch(new UiDialogType(dialogType));
        }

        /// <inheritdoc />
        public void Hide(UiDialogType dialogType)
        {
            signalBus.Fire(new OnHideUiDialogSignal(dialogType));
        }

        /// <inheritdoc />
        public IUIDialogLaunchSetting Prepare(UiDialogType dialogType)
        {
            var prefab = FindPrefab(dialogType);

            if (prefab == null)
            {
                HMLog.LogWarning($"[IUIDialogLauncher]未找到[{dialogType}]对应的prefab");

                return NullDlgSetting;
            }

            return dlgSettingFactory.Create(dialogType, prefab);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private GameObject FindPrefab(UiDialogType type)
        {
            foreach (var mapping in mappings)
            {
                if (type.Name.Equals(mapping.Type))
                {
                    return mapping.Prefab;
                }
            }

            return null;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649