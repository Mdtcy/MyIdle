using UnityEngine;

namespace DefaultNamespace.Item
{
    [CreateAssetMenu(fileName = "ItemContent", menuName = "Config/ItemContent", order = 0)]
    public class ItemContent : ScriptableObject
    {
        public ItemDef ItemAtk;

        public ItemDef ItemAtkSpeed;
    }
}