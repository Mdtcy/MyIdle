/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-05 09:05:12
 * @modify date 2020-05-05 09:05:12
 * @desc [description]
 */

namespace HM.GameBase
{
    public class ItemGetter : IItemGetter
    {
        private readonly Inventory inventory;

        public ItemGetter(Inventory inv)
        {
            inventory = inv;
        }

        public T GetItem<T>(int itemId) where T : ItemBase
        {
            return inventory.GetItem<T>(itemId);
        }

        public bool HasItem(int itemId)
        {
            return inventory.HasItem(itemId);
        }

        /// <inheritdoc />
        public int ItemCount(int itemId)
        {
            var item = GetItem<ItemBase>(itemId);
            return item?.Num ?? 0;
        }
    }
}