/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-05 11:03:08
 * @modify date 2020-03-05 11:03:08
 * @desc [description]
 */

using UnityEngine;

namespace HM.GameBase
{
    public partial class UiManager
    {
        public UiDialog Prepare(GameObject prefab, bool recycleEnabled)
        {
            return GetOrCreateDialogue(prefab, recycleEnabled);
        }

        public UiDialogHandle Show(UiDialog        dialog,
                                   object          param,
                                   ShowModalOption option = ShowModalOption.BlackCurtain)
        {
            ShowCurtain(option);

            return ShowModalInternal(dialog, false, param, option);
        }

        /// <summary>
        /// 显示指定prefab
        /// </summary>
        /// <param name="prefab">必须包含UiDialog类型的component</param>
        /// <param name="recycleEnabled">是否复用，如果是，关闭后不会销毁该对象而是暂存起来</param>
        /// <param name="param">在OnWillShow/OnDidShow回调时会将param传回</param>
        /// <param name="option">默认显示黑色半透遮罩</param>
        /// <returns></returns>
        public UiDialogHandle ShowModalPrefab(GameObject      prefab,
                                              bool            recycleEnabled,
                                              object          param,
                                              ShowModalOption option = ShowModalOption.BlackCurtain)
        {
            ShowCurtain(option);

            return ShowModalInternal(GetOrCreateDialogue(prefab, recycleEnabled), !recycleEnabled, param, option);
        }
    }
}