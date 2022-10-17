/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-05 13:33:54
 * @modify date 2022-09-05 13:33:54
 * @desc [游戏获得/失去焦点的信号]
 */

namespace NewLife.Defined.Signals
{
    public class OnGameFocusChangedSignal
    {
        public bool HasFocus { get; }

        public OnGameFocusChangedSignal(bool hasFocus)
        {
            HasFocus = hasFocus;
        }
    }
}