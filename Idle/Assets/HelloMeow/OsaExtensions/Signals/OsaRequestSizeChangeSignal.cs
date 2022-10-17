/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-11-10 15:11:19
 * @modify date 2021-11-10 15:11:19
 * @desc [更新指定cell的size(Height/Width，视滚动方向而定)]
 */

namespace HM.OsaExtensions
{
    public class OsaRequestSizeChangeSignal
    {
        /// <summary>
        /// cell索引
        /// </summary>
        public readonly int ItemIndex;

        /// <summary>
        /// 新的size
        /// </summary>
        public readonly float NewSize;

        public OsaRequestSizeChangeSignal(int itemIndex, float newSize)
        {
            ItemIndex = itemIndex;
            NewSize   = newSize;
        }
    }
}