using System;
using System.Collections.Generic;

namespace HM.GameBase
{
    public interface IItemListGetter
    {
        void GetItems<T>(ref List<T> list) where T : ItemBase;
        void FilterItems<T>(Func<T, bool> fn, ref List<T> items) where T : ItemBase;
    }
}