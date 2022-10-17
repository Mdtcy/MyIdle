/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-08 13:07:43
 * @modify date 2020-07-08 13:07:43
 * @desc [description]
 */

namespace HM.GameBase
{
    public interface IItemRemover
    {
        /// <summary>
        /// 删除指定item
        /// </summary>
        /// <param name="itemId"></param>
        void RemoveItem(int itemId);
    }
}
