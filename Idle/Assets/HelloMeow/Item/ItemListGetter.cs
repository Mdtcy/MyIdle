/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-05 12:05:24
 * @modify date 2020-05-05 12:05:24
 * @desc [description]
 */

using System;
using System.Collections.Generic;

namespace HM.GameBase
{
    public class ItemListGetter : IItemListGetter
    {
        private readonly Inventory inventory;

        public ItemListGetter(Inventory inv)
        {
            inventory = inv;
        }

        public void GetItems<T>(ref List<T> list) where T : ItemBase
        {
            inventory.FilterItems(ref list);
        }

        public void FilterItems<T>(Func<T, bool> fn, ref List<T> items) where T : ItemBase
        {
            inventory.FilterItems(fn, ref items);
        }
    }
}