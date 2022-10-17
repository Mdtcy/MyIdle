/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-30 10:46:25
 * @modify date 2022-08-30 10:46:25
 * @desc [打开UiDialog时需要做的设置]
 */

using System;
using HM.GameBase;

namespace NewLife.Defined.Interfaces
{
    public interface IUIDialogLaunchSetting
    {
        /// <summary>
        /// 设置传入参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IUIDialogLaunchSetting SetArguments(params object[] param);

        /// <summary>
        /// 设置关闭时的回调
        /// </summary>
        /// <param name="onQuitCallback"></param>
        /// <returns></returns>
        IUIDialogLaunchSetting SetQuitCallback(Action onQuitCallback);

        /// <summary>
        /// 设置显示选项
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        IUIDialogLaunchSetting SetShowOption(UiManager.ShowModalOption option);

        /// <summary>
        /// 设置显示层级
        /// </summary>
        /// <param name="displayLevel"></param>
        /// <returns></returns>
        IUIDialogLaunchSetting SetDisplayLevel(UiDialogDisplayLevel displayLevel);

        /// <summary>
        /// 打开uiDialog
        /// </summary>
        void Launch();
    }
}