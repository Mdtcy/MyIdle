/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-05 12:05:51
 * @modify date 2020-05-05 12:05:51
 * @desc [description]
 */

using HM.Interface;

namespace HM.GameBase
{
    public class ItemUpdater : ItemGetter, IItemUpdater
    {
        private readonly IRequest request;
        public ItemUpdater(Inventory inv, IRequest request) : base(inv)
        {
            this.request = request;
        }

        /// <inheritdoc />
        public void AddItem(int itemId, int num)
        {
            request.AcquireItem(itemId, num);
        }

        /// <inheritdoc />
        public void Acquire(ItemParams itemParams)
        {
            if (itemParams == null)
            {
                return;
            }

            foreach (var data in itemParams)
            {
                AddItem(data.ItemId, data.Num);
            }
        }

        /// <inheritdoc />
        public void ConsumeItem(int itemId, int num)
        {
            request.ConsumeItem(itemId, num);
        }

        /// <inheritdoc />
        public T GetOrAddItem<T>(int itemId) where T : ItemBase
        {
            if (!HasItem(itemId))
            {
                AddItem(itemId, 1);
            }
            return GetItem<T>(itemId);
        }
    }
}