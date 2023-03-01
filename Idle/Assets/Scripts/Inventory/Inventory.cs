/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月21日
 * @modify date 2023年2月21日
 * @desc []
 */

#pragma warning disable 0649
using System.Collections.Generic;
using DefaultNamespace.Item;
using UnityEngine;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour
    {
        #region FIELDS

        private Dictionary<string, int> items = new Dictionary<string, int>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void AddItem(string item, int count)
        {
            if (items.ContainsKey(item))
            {
                items[item] += count;
            }
            else
            {
                items.Add(item, count);
            }
        }

        public void AddItem(ItemDef item, int count)
        {
            AddItem(item.Name, count);
        }

        public int GetItemCount(string item)
        {
            if (items.ContainsKey(item))
            {
                return items[item];
            }

            return 0;
        }

        public int GetItemCount(ItemDef item)
        {
            return GetItemCount(item.Name);
        }


        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649