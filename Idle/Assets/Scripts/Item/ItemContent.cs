using UnityEngine;

namespace DefaultNamespace.Item
{
    [CreateAssetMenu(fileName = "ItemContent", menuName = "Config/ItemContent", order = 0)]
    public class ItemContent : ScriptableObject
    {
        public ItemDef ItemHealOnHit;

        public ItemDef ItemAtkSpeed;

        public ItemDef Item80HpAddDamage;

        public ItemDef Item10CritChance;

        public ItemDef Item20DamageAround;
    }
}