/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc []
 */

#pragma warning disable 0649
using System;
using Damage;
using HM;
using Numeric;
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

        public Action ActOnAttributeChanged;
        public Action ActOnResourceChanged;

        public BuffComponent BuffComponent;

        private ChaControlState controlState = new ChaControlState(true, true);

        // local

        // 属性
        private AttributesNumeric attributesNumeric;

        // 资源
        private ResourceNumeric resourceNumeric;

        #endregion

        #region PROPERTIES

        public ChaControlState ControlState => controlState;

        #endregion

        #region PUBLIC METHODS

        // public void Attack(Entity other)
        // {
        //     // 闪避判断
        //     if (Random.Range(0f, 1f) <= attributesNumeric.Get(AttributeType.DodgeProbability))
        //     {
        //         other.OnMiss();
        //     }
        //     // 命中敌人
        //     else
        //     {
        //         float damage = attributesNumeric.Get(AttributeType.Atk);
        //
        //         bool isCritical = false;
        //         if (Random.Range(0f, 1f) <= attributesNumeric.Get(AttributeType.CriticalProbability))
        //         {
        //             damage     *= attributesNumeric.Get(AttributeType.CriticalDamage);
        //             isCritical =  true;
        //         }
        //
        //         if (isCritical)
        //         {
        //             // criticalDamage.Spawn(other.transform.position + new Vector3(0, 0.5f, 0), $"-{damage}");
        //         }
        //         else
        //         {
        //             // normalDamage.Spawn(other.transform.position + new Vector3(0, 0.5f, 0), $"-{damage}");
        //         }
        //
        //         other.OnHurt(damage);
        //     }
        // }

        // public void OnHurt(float damage)
        // {
        //     if (damage >= resourceNumeric.Get(ResourceType.Hp))
        //     {
        //         OnDeath();
        //     }
        //     else
        //     {
        //         ChangedHp(-damage);
        //     }
        // }

        // public void ChangedHp(float changed)
        // {
        //     float hp    = resourceNumeric.Get(ResourceType.Hp);
        //     float maxHp = attributesNumeric.Get(AttributeType.MaxHp);
        //     //
        //     // float hpRatioBeforeHpChanged = hp             / maxHp;
        //     // float hpRatioAfterHpChanged  = (hp + changed) / maxHp;
        //     ModifyResource(ResourceType.Hp, changed, ModifyNumericType.Add);
        //     // if (hpRatioBeforeHpChanged < 0.3f && hpRatioAfterHpChanged >= 0.3f)
        //     // {
        //     //     BuffComponent.TriggerEvent(new EEventHpHigher30Percent());
        //     // }
        //     // else if (hpRatioBeforeHpChanged >= 0.3f && hpRatioAfterHpChanged < 0.3f)
        //     // {
        //     //     BuffComponent.TriggerEvent(new EEventHpLower30Percent());
        //     // }
        // }

        // public void OnDeath()
        // {
        //     ModifyResource(ResourceType.Hp, 0f, ModifyNumericType.Set);
        //     Destroy(gameObject);
        // }
        //
        // public void OnMiss()
        // {
        //     Debug.Log($"{name} Miss");
        // }

        ///<summary>
        ///角色的无敌状态持续时间，如果在无敌状态中，子弹不会碰撞，DamageInfo处理无效化
        ///单位：秒
        ///</summary>
        public float immuneTime
        {
            get { return _immuneTime; }
            set { _immuneTime = Mathf.Max(_immuneTime, value); }
        }

        private float _immuneTime = 0.00f;

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public float GetAttribute(AttributeType attributeType)
        {
            return attributesNumeric.Get(attributeType);
        }

        /// <summary>
        /// 攻击间隔
        /// </summary>
        /// <returns></returns>
        public float GetFireInterval()
        {
            float attackSpeed      = GetAttribute(AttributeType.AttackSpeed);
            float baseFireInterval = GetAttribute(AttributeType.BaseFireInterval);

            // todo 每次攻击的时间 = BAT / [(初始攻击速度 + IAS) × 0.01] = 1 / (每秒攻击的次数) 100 是基础攻速
            // todo https://dota2.fandom.com/zh/wiki/%E6%94%BB%E5%87%BB%E9%80%9F%E5%BA%A6?variant=zh
            float fireInterval = baseFireInterval / ((attackSpeed + 100) * 0.01f);

            return fireInterval;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public float GetResource(ResourceType resourceType)
        {
            return resourceNumeric.Get(resourceType);
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="value"></param>
        /// <param name="modifyNumericType"></param>
        public void ModifyAttribute(AttributeType attributeType, float value, ModifyNumericType modifyNumericType)
        {
            switch (modifyNumericType)
            {
                case ModifyNumericType.Add:
                {
                    attributesNumeric.Add(attributeType, value);
                    break;
                }
                case ModifyNumericType.Set:
                {
                    attributesNumeric.Set(attributeType, value);
                    break;
                }
                default:
                    HMLog.LogError("未定义的ModifyNumericType");
                    break;
            }

            // todo 放数值组件里吧
            ActOnAttributeChanged?.Invoke();
        }

        /// <summary>
        /// 修改资源
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="value"></param>
        /// <param name="modifyNumericType"></param>
        public void ModifyResource(ResourceType resourceType, float value, ModifyNumericType modifyNumericType)
        {
            switch (modifyNumericType)
            {
                case ModifyNumericType.Add:
                {
                    if (resourceNumeric.Get(ResourceType.Hp) + value > attributesNumeric.Get(AttributeType.MaxHp))
                    {
                        resourceNumeric.Set(ResourceType.Hp,attributesNumeric.Get(AttributeType.MaxHp));
                    }
                    else
                    {
                        resourceNumeric.Add(resourceType, value);
                    }

                    break;
                }
                case ModifyNumericType.Set:
                {
                    if (value > attributesNumeric.Get(AttributeType.MaxHp))
                    {
                        resourceNumeric.Set(ResourceType.Hp, attributesNumeric.Get(AttributeType.MaxHp));
                    }
                    else
                    {
                        resourceNumeric.Set(resourceType, value);
                    }

                    break;
                }
                default:
                    HMLog.LogError("未定义的ModifyNumericType");

                    break;
            }

            if (this.resourceNumeric.Get(ResourceType.Hp) <= 0)
            {
                this.Kill();
            }

            ActOnResourceChanged?.Invoke();
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

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        // 初始
        public void Init(EntityConfig entityConfig)
        {
            BuffComponent.Init(this);

            InitAttr(entityConfig);
            InitResource();
        }

        // 初始属性
        private void InitAttr(EntityConfig entityConfig)
        {
            attributesNumeric = new AttributesNumeric();

            foreach (var attrValue in entityConfig.InitAttrs)
            {
                ModifyAttribute(attrValue.AttributeType, attrValue.Value, ModifyNumericType.Set);
            }
        }

        // 初始资源
        private void InitResource()
        {
            resourceNumeric = new ResourceNumeric();
            ModifyResource(ResourceType.Hp, attributesNumeric.Get(AttributeType.MaxHp), ModifyNumericType.Set);
        }

        #endregion

        #region STATIC METHODS

        #endregion

        public bool CanBeKilledByDamageInfo(DamageInfo damageInfo, int value)
        {
            if (this.immuneTime > 0 || damageInfo.isHeal() == true) return false;
            // int dValue = damageInfo.DamageValue(false);

            // return dValue >= this.resource.hp;
            // return dValue >= resourceNumeric.Get(ResourceType.Hp);
            return value >= resourceNumeric.Get(ResourceType.Hp);
        }

        // 状态计算
        public void ControlStateRecheck()
        {
            controlState.Origin();

            for (int i = 0; i < this.BuffComponent.Buffs.Count; i++)
            {
                var buff = BuffComponent.Buffs[i];
                controlState += buff.model.stateMod;
            }
        }
    }
}
#pragma warning restore 0649