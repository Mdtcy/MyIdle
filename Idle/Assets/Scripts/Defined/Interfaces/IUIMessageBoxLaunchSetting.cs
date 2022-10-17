/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-05 14:11:21
 * @modify date 2022-09-05 14:11:21
 * @desc [description]
 */

using System;

namespace NewLife.Defined.Interfaces
{
    public interface IUIMessageBoxLaunchSetting : IUIDialogLaunchSetting
    {
        /// <summary>
        /// 设置确认回调
        /// 需要对应的uiDialog类型为UIMessageBox
        /// </summary>
        /// <param name="onConfirm"></param>
        /// <returns></returns>
        IUIMessageBoxLaunchSetting SetConfirmCallback(Action onConfirm);

        /// <summary>
        /// 设置确认回调
        /// 需要对应的uiDialog类型为UIMessageBox
        /// </summary>
        /// <param name="onCancel"></param>
        /// <returns></returns>
        IUIMessageBoxLaunchSetting SetCancelCallback(Action onCancel);

        /// <summary>
        /// 隐藏取消按钮（默认显示）
        /// 需要对应的uiDialog类型为UIMessageBox
        /// </summary>
        /// <returns></returns>
        IUIMessageBoxLaunchSetting HideCancelButton();

        /// <summary>
        /// 设置标题
        /// 需要对应的uiDialog类型为UIMessageBox
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IUIMessageBoxLaunchSetting SetTitle(string title);

        /// <summary>
        /// 设置文本
        /// 需要对应的uiDialog类型为UIMessageBox
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        IUIMessageBoxLaunchSetting SetContent(string content);

    }
}