using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Item
{
    [CreateAssetMenu(fileName = "ItemDef", menuName = "DEF/ItemDef", order = 0)]
    public class ItemDef : ScriptableObject
    {
        public Sprite SpIcon;

        public string Name;

        public string Description;

        public List<ItemTag> Tags;
        public ItemTier      Tier;


        private ItemIndex itemIndex;
        public ItemIndex ItemIndex
        {
            get
            {
                if (this.itemIndex == ItemIndex.None)
                {
                    Debug.LogError("ItemDef '" + base.name + "' has an item index of 'None'.  Attempting to fix...");
                    this.itemIndex = ItemCatalog.FindItemIndex(base.name);

                    if (this.itemIndex != ItemIndex.None)
                    {
                        Debug.LogError(string.Format("Able to fix ItemDef '{0}' (item index = {1}).  This is probably because the asset is being duplicated across bundles.",
                                                     base.name, this.itemIndex));
                    }
                }

                return this.itemIndex;
            }
            set => this.itemIndex = value;
        }

        public bool ContainsTag(ItemTag tag)
        {
            return tag == ItemTag.Any || Tags.Contains(tag);
        }
    }
}