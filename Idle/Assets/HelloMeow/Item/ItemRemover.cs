/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-08 13:07:04
 * @modify date 2020-07-08 13:07:04
 * @desc [删除指定item]
 */

using HM.Interface;

namespace HM.GameBase
{
    public class ItemRemover : IItemRemover
    {
        private readonly IRequest request;

        public ItemRemover(IRequest request)
        {
            this.request = request;
        }

        /// <inheritdoc />
        public void RemoveItem(int itemId)
        {
            request.RemoveItem(itemId);
        }
    }
}
