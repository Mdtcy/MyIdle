/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月7日
 * @modify date 2023年2月7日
 * @desc [ItemCatalog]
 */

using System;
using System.Collections.Generic;

#pragma warning disable 0649
namespace DefaultNamespace.Item
{
    public static class ItemCatalog
    {
        #region FIELDS

        public static List<ItemIndex> tier1ItemList = new List<ItemIndex>();

        public static List<ItemIndex> tier2ItemList = new List<ItemIndex>();

        public static List<ItemIndex> tier3ItemList = new List<ItemIndex>();

        private static List<ItemDef> itemDefs = new List<ItemDef>();

        public static List<string> itemNames = new List<string>();

        private static readonly Dictionary<string, ItemIndex> itemNameToIndex = new Dictionary<string, ItemIndex>();

        private static Dictionary<ItemTag, List<ItemIndex>> itemIndicesByTag = new Dictionary<ItemTag, List<ItemIndex>>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public static void SetItemDefs(List<ItemDef> newItemDefs)
        {
            itemDefs.Clear();
            // Clone
            foreach (var itemDef in newItemDefs)
            {
                itemDefs.Add(itemDef);
            }

            itemNameToIndex.Clear();
            for (int index = 0; index < itemDefs.Count; index++)
            {
                itemNames.Add(itemDefs[index].name);
            }

            itemNames.Sort(StringComparer.Ordinal);
            itemDefs.Sort((ItemDef a, ItemDef b) => String.Compare(a.name, b.name, StringComparison.Ordinal));
            for (ItemIndex itemIndex = (ItemIndex) 0; itemIndex < (ItemIndex) ItemCatalog.itemDefs.Count; itemIndex++)
            {
                ItemDef itemDef = itemDefs[(int) itemIndex];
                string  key     = itemNames[(int) itemIndex];
                itemDef.ItemIndex = itemIndex;

                switch (itemDef.Tier)
                {
                    case ItemTier.Tier1:
                        ItemCatalog.tier1ItemList.Add(itemIndex);

                        break;
                    case ItemTier.Tier2:
                        ItemCatalog.tier2ItemList.Add(itemIndex);

                        break;
                    case ItemTier.Tier3:
                        ItemCatalog.tier3ItemList.Add(itemIndex);

                        break;
                }

                itemNameToIndex[key] = itemIndex;
            }

            itemIndicesByTag.Clear();
            foreach (var itemDef in itemDefs)
            {
                foreach (var itemTag in itemDef.Tags)
                {
                    if(itemIndicesByTag.ContainsKey(itemTag))
                    {
                        itemIndicesByTag[itemTag].Add(itemDef.ItemIndex);
                    }
                    else
                    {
                        itemIndicesByTag[itemTag] = new List<ItemIndex> {itemDef.ItemIndex};
                    }
                }
            }
        }

        public static ItemDef GetItemDef(ItemIndex itemIndex)
        {
            return itemDefs[(int)itemIndex];
        }

        public static ItemIndex FindItemIndex(string itemName)
        {
            ItemIndex result;

            if (itemNameToIndex.TryGetValue(itemName, out result))
            {
                return result;
            }

            return ItemIndex.None;
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