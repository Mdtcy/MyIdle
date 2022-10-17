/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-07 17:24:10
 * @modify date 2022-09-07 17:24:10
 * @desc [隐藏指定类型的UiDialog]
 */

using NewLife.Defined;

namespace NewLife.UI.DialogLauncher
{
    public class OnHideUiDialogSignal
    {
        public UiDialogType DialogType { get; }

        public OnHideUiDialogSignal(UiDialogType dialogType)
        {
            DialogType = dialogType;
        }
    }
}