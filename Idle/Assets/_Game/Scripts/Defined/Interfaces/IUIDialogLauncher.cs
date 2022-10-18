/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-30 10:36:41
 * @modify date 2022-08-30 10:36:41
 * @desc [打开指定ui对话框]
 */

namespace NewLife.Defined.Interfaces
{
    public interface IUIDialogLauncher
    {
        /// <summary>
        /// 直接打开指定dialog
        /// </summary>
        /// <param name="dialogType"></param>
        /// <returns></returns>
        void Launch(UiDialogType dialogType);
        /// <summary>
        /// 直接打开指定dialog
        /// </summary>
        /// <param name="dialogType"></param>
        /// <returns></returns>
        void Launch(string dialogType);

        /// <summary>
        /// 准备指定dialog
        /// </summary>
        /// <param name="dialogType"></param>
        /// <returns></returns>
        IUIDialogLaunchSetting Prepare(UiDialogType dialogType);

        /// <summary>
        /// 隐藏指定类型的弹板
        /// </summary>
        /// <param name="dialogType"></param>
        void Hide(UiDialogType dialogType);
    }
}