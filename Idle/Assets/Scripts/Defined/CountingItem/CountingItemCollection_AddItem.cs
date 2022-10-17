/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-03-02 14:07:46
 * @modify date 2022-03-02 14:07:46
 * @desc [CountingItemCollection扩展-获得包含的全部物品]
 */

using HM.GameBase;
using Zenject;

namespace NewLife.Defined
{
    public partial class CountingItemCollection
    {
        [Inject]
        private IItemUpdater itemUpdater;

        /// <summary>
        /// 获得全部物品
        /// </summary>
        public void Acquire()
        {
            foreach (var item in items)
            {
                itemUpdater.AddItem(item.Item.Id, item.Num);
            }
        }
    }
}