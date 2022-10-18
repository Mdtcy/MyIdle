/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-30 10:50:16
 * @modify date 2022-08-30 10:50:16
 * @desc [UiDialog设置项]
 */

using System;
using HM;
using HM.GameBase;
using NewLife.Defined;
using NewLife.Defined.Interfaces;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.UI.DialogLauncher
{
    public class UIDialogLaunchSetting : IUIDialogLaunchSetting
    {
        #region FIELDS

        // dialog 类型
        private readonly UiDialogType dialogType;

        // dialog prefab
        private readonly GameObject pfbDialog;

        // 传给dialog的参数
        private object[] param;

        // dialog关闭的回调
        private Action onQuit;

        // 显示层级
        private UiDialogDisplayLevel displayLevel;

        private UiDialogHandle handle;

        // 显示选项
        private UiManager.ShowModalOption option = UiManager.ShowModalOption.BlackCurtain |
                                                   UiManager.ShowModalOption.TransparentBlock;

        // * injected
        private readonly UiManager uiManagerDefault;
        private readonly UiManager uiManagerPopup;
        private readonly SignalBus signalBus;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <inheritdoc />
        public IUIDialogLaunchSetting SetArguments(params object[] param)
        {
            this.param = param;

            return this;
        }

        /// <inheritdoc />
        public IUIDialogLaunchSetting SetQuitCallback(Action onQuit)
        {
            this.onQuit = onQuit;

            return this;
        }

        /// <inheritdoc />
        public IUIDialogLaunchSetting SetShowOption(UiManager.ShowModalOption option)
        {
            this.option = option;

            return this;
        }

        /// <inheritdoc />
        public IUIDialogLaunchSetting SetDisplayLevel(UiDialogDisplayLevel displayLevel)
        {
            this.displayLevel = displayLevel;

            return this;
        }

        /// <inheritdoc />
        public void Launch()
        {
            HMLog.LogDebug($"{this}::Launch");

            if (pfbDialog == null)
            {
                HMLog.LogWarning($"[IUIDialogLaunchSetting]prefab == null");

                return;
            }

            var paramToPass = param == null || param.Length == 0 ? null : param.Length == 1 ? param[0] : param;

            var dialog = GetUiManager().Prepare(pfbDialog, true);
            OnWillShow(dialog);
            handle = GetUiManager().Show(dialog, paramToPass, option);

            handle.Hidden += OnDialogHidden;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[UIDialogLaunchSetting {dialogType} / {(pfbDialog != null ? pfbDialog.name : "NULL")}]";
        }

        #endregion

        #region PROTECTED METHODS

        protected UIDialogLaunchSetting(UiDialogType dialogType,
                                        GameObject   pfbDialog,
                                        SignalBus    signalBus,
                                        UiManager    uiManagerDefault,
                                        UiManager    uiManagerPopup)
        {
            this.dialogType       = dialogType;
            this.pfbDialog        = pfbDialog;
            this.signalBus        = signalBus;
            this.uiManagerDefault = uiManagerDefault;
            this.uiManagerPopup   = uiManagerPopup;
        }

        protected virtual void OnWillShow(UiDialog dialog)
        {
            signalBus.Subscribe<OnHideUiDialogSignal>(OnHideDialog);
        }

        private void OnHideDialog(OnHideUiDialogSignal sig)
        {
            if (sig.DialogType == dialogType)
            {
                handle?.Dialog.Hide();
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void OnDialogHidden(UiDialog arg1, object arg2)
        {
            signalBus.Unsubscribe<OnHideUiDialogSignal>(OnHideDialog);
            onQuit?.Invoke();
        }

        private UiManager GetUiManager()
        {
            return displayLevel == UiDialogDisplayLevel.Desktop ? uiManagerDefault : uiManagerPopup;
        }

        #endregion

        #region STATIC METHODS

        #endregion

        public class Factory
        {
            private readonly UiManager uiManagerDefault;
            private readonly UiManager uiManagerPopup;
            private readonly SignalBus signalBus;

            [Inject]
            public Factory(UiManager uiManager,
                           [Inject(Id = ZenjectId.UiManagerDialogPopup)]
                           UiManager uiManagerPopup,
                           SignalBus signalBus)
            {
                this.uiManagerPopup   = uiManagerPopup;
                this.uiManagerDefault = uiManager;
                this.signalBus        = signalBus;
            }

            public IUIDialogLaunchSetting Create(UiDialogType dialogType, GameObject prefab)
            {
                return new UIDialogLaunchSetting(dialogType, prefab, signalBus, uiManagerDefault, uiManagerPopup);
            }
        }
    }
}
#pragma warning restore 0649