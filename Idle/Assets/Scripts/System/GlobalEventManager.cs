/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月14日
 * @modify date 2023年2月14日
 * @desc []
 */

using Damage;
using DefaultNamespace.Game;
using DefaultNamespace.Item;
using IdleGame;
using UnityEngine;

#pragma warning disable 0649
namespace DefaultNamespace.System
{
    public class GlobalEventManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private float damageForce;

        #endregion

        #region PROPERTIES

        private Inventory inventory => GameManager.Instance.Inventory;

        private ItemContent itemContent => GameManager.Instance.ItemContent;

        #endregion

        #region PUBLIC METHODS

        public void OnHitEnemy(Entity attacker, Entity defender)
        {
            float damage = attacker.atk;

            // 对生命值80%以上的敌人造成170%伤害（叠加 75%伤
            if (defender.hp / defender.maxHp > 0.8f)
            {
                int itemCount = inventory.GetItemCount(itemContent.Item80HpAddDamage);

                if (itemCount > 0)
                {
                    damage = damage * (1.7f + 0.7f * (itemCount - 1));
                }
            }

            // 每个叠加的 10%暴击几率，双倍伤害
            int item10CritChanceCount = inventory.GetItemCount(itemContent.Item10CritChance);
            if (item10CritChanceCount > 0 && Random.Range(0, 100) < 10 * item10CritChanceCount)
            {
                damage *= 2;
            }

            // 对周围的敌人造成的伤害提高 20%（每层 +20%）
            int item20DamageAroundCount = inventory.GetItemCount(itemContent.Item20DamageAround);
            if (item20DamageAroundCount > 0 && Vector2.Distance(attacker.gameObject.transform.position,
                                                               defender.gameObject.transform.position) < 3.5)
            {
                damage *= 1 + 0.2f * item20DamageAroundCount;
            }

            int itemHealOnHitCount = inventory.GetItemCount(itemContent.ItemHealOnHit);

            // todo 回血

            SceneVariants.CreateDamage(attacker.gameObject,
                                       defender.gameObject,
                                       new Damage.Damage((int) damage),
                                       transform.eulerAngles.z,
                                       new DamageInfoTag[] {DamageInfoTag.directDamage,}
                                      );

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