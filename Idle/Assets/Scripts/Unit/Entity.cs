/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc []
 */

#pragma warning disable 0649
using Damage;
using Sirenix.OdinInspector;
using Unit;
using UnityEngine;

namespace IdleGame
{
    public class Entity : MonoBehaviour
    {
        #region FIELDS

        public string name;

        public Side side;

        [ReadOnly]
        public bool dead;

        public BuffComponent BuffComponent;

        private ChaControlState controlState = new ChaControlState(true, true);

        // local

        // 属性
        public float atk;
        public float critChance;
        public float critDamage;
        public float dodgeChance;
        public float attackSpeed;
        public float baseFireRate;
        public float hp;
        public float maxHp;

        #endregion

        #region PROPERTIES

        public ChaControlState ControlState => controlState;

        #endregion

        #region PUBLIC METHODS

        public void TakeDamage(float damage)
        {
            if (damage >= hp)
            {
                Kill();
            }
            else
            {
                hp -= (int) damage;
            }
        }

        ///<summary>
        ///杀死这个角色
        ///</summary>
        public void Kill()
        {
            this.dead = true;

            // todo 死亡动画
            // if (unitAnim)
            // {
            //     unitAnim.Play("Dead");
            // }

            // //如果不是主角，尸体就会消失
            // if (this.gameObject != SceneVariants.MainActor())
            //     this.gameObject.AddComponent<UnitRemover>().duration = 5.0f;
            Destroy(gameObject);
        }

        ///<summary>
        ///角色的无敌状态持续时间，如果在无敌状态中，子弹不会碰撞，DamageInfo处理无效化
        ///单位：秒
        ///</summary>
        public float ImmuneTime
        {
            get { return immuneTime; }
            set { immuneTime = Mathf.Max(immuneTime, value); }
        }

        private float immuneTime = 0.00f;

        /// <summary>
        /// 攻击间隔
        /// </summary>
        /// <returns></returns>
        public float GetFireInterval()
        {
            // todo 每次攻击的时间 = BAT / [(初始攻击速度 + IAS) × 0.01] = 1 / (每秒攻击的次数) 100 是基础攻速
            // todo https://dota2.fandom.com/zh/wiki/%E6%94%BB%E5%87%BB%E9%80%9F%E5%BA%A6?variant=zh
            float fireInterval = baseFireRate / ((attackSpeed + 100) * 0.01f);

            return fireInterval;
        }


        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region STATIC METHODS

        #endregion

        public bool CanBeKilledByDamageInfo(DamageInfo damageInfo, int value)
        {
            // todo
            return false;

            // if (this.immuneTime > 0 || damageInfo.isHeal() == true) return false;
            // // int dValue = damageInfo.DamageValue(false);
            //
            // // return dValue >= this.resource.hp;
            // // return dValue >= resourceNumeric.Get(ResourceType.Hp);
            // return value >= resourceNumeric.Get(ResourceType.Hp);
        }

        // 状态计算
        public void ControlStateRecheck()
        {
            // controlState.Origin();
            //
            // for (int i = 0; i < this.BuffComponent.Buffs.Count; i++)
            // {
            //     var buff = BuffComponent.Buffs[i];
            //     controlState += buff.model.stateMod;
            // }
        }
    }
}
#pragma warning restore 0649