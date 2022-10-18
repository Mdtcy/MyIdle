/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-14 21:57:16
 * @modify date 2018-12-14 21:57:16
 * @desc [Item工厂类，创建Item的唯一方式（除序列化之外）]
 */

using HM;
using HM.GameBase;
using HM.Interface;
using Zenject;

namespace NewLife.BusinessLogic.Item
{
    /// <summary>
    /// Item工厂类，创建Item的唯一方式（除序列化之外）
    /// </summary>
    public class ItemFactory : IItemFactory
    {
        private readonly DiContainer diContainer;
        private readonly IConfigChecker configChecker;

        public ItemFactory(DiContainer container, IConfigChecker configChecker)
        {
            diContainer = container;
            this.configChecker = configChecker;
        }
        /// <summary>
        /// 根据ItemId的类型创建并返回相应的item
        /// </summary>
        /// <param name="itemId">要创建的Item的Id</param>
        /// <returns>返回新创建的Item</returns>
        public ItemBase CreateItem(int itemId)
        {
            ItemBase item = null;
            // todo: fill Item creation code.

            // if (configChecker.IsItemSecret(itemId)) item = new ItemSecret(itemId);

            if (item == null)
            {
                HMLog.LogDebug($"创建ItemBase[id = {itemId}]");
                item = new ItemBase(itemId);
            }
            diContainer.Inject(item);
            item.Setup(itemId);
            return item;
        }
    }
}