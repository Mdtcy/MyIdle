/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-01 23:05:28
 * @modify date 2020-05-01 23:05:28
 * @desc [description]
 */

#if HM_ZENJECT
using Zenject;
#endif

namespace HM.GameBase
{
    public partial class UiDialog
    {
#if HM_ZENJECT
        public class Factory : PlaceholderFactory<UnityEngine.GameObject, UiDialog>, UiManager.IUiDialogFactory
        {
        }
#endif
    }
}