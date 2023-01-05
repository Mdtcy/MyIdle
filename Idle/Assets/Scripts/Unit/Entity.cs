/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using DamageNumbersPro;
using Event;
using HM;
using Numeric;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IdleGame
{
    public class Entity : MonoBehaviour
    {
        #region FIELDS

        [BoxGroup("Floating Text")]
        [SerializeField]
        private DamageNumber normalDamage;

        [BoxGroup("Floating Text")]
        [SerializeField]
        private DamageNumber criticalDamage;

        [BoxGroup("Floating Text")]
        [SerializeField]
        private DamageNumber missDamage;

        public string name;

        public Side side;


        public BuffComponent BuffComponent;

        // local
        // 属性
        private AttributesNumeric attributesNumeric;

        // 资源
        private ResourceNumeric resourceNumeric;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Attack(Entity other)
        {
            // 闪避判断
            if (Random.Range(0f, 1f) <= attributesNumeric.Get(AttributeType.DodgeProbability))
            {
                other.OnMiss();
                missDamage.Spawn(other.transform.position + new Vector3(0, 0.5f, 0), $"miss");
            }
            // 命中敌人
            else
            {
                float damage = attributesNumeric.Get(AttributeType.Atk);

                bool isCritical = false;
                if (Random.Range(0f, 1f) <= attributesNumeric.Get(AttributeType.CriticalProbability))
                {
                    damage     *= attributesNumeric.Get(AttributeType.CriticalDamage);
                    isCritical =  true;
                }

                if (isCritical)
                {
                    criticalDamage.Spawn(other.transform.position + new Vector3(0, 0.5f, 0), $"-{damage}");
                }
                else
                {
                    normalDamage.Spawn(other.transform.position + new Vector3(0, 0.5f, 0), $"-{damage}");
                }

                other.OnHurt(damage);
            }
        }

        public void OnHurt(float damage)
        {
            if (damage >= resourceNumeric.Get(ResourceType.Hp))
            {
                OnDeath();
            }
            else
            {
                ChangedHp(-damage);
            }
        }

        public void ChangedHp(float changed)
        {
            float hp    = resourceNumeric.Get(ResourceType.Hp);
            float maxHp = attributesNumeric.Get(AttributeType.MaxHp);

            float hpRatioBeforeHpChanged = hp             / maxHp;
            float hpRatioAfterHpChanged  = (hp + changed) / maxHp;
            resourceNumeric.Add(ResourceType.Hp, changed);

            if (hpRatioBeforeHpChanged < 0.3f && hpRatioAfterHpChanged >= 0.3f)
            {
                BuffComponent.TriggerEvent(new EEventHpHigher30Percent());
            }
            else if (hpRatioBeforeHpChanged >= 0.3f && hpRatioAfterHpChanged < 0.3f)
            {
                BuffComponent.TriggerEvent(new EEventHpLower30Percent());
            }
        }

        public void OnDeath()
        {
            Destroy(gameObject);
        }

        public void OnMiss()
        {
            Debug.Log($"{name} Miss");
        }

        [SerializeField]
        private Transform pfbBullet;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private float fireInterval = 0.3f;

        public float shootTimer;

        public bool canShoot;
        private void Update()
        {
            if (!canShoot)
            {
                return;
            }

            shootTimer -= Time.deltaTime;
            while (shootTimer <= 0)
            {
                var entitys = FindObjectsOfType<Entity>();

                if (entitys.Length > 0)
                {
                    foreach (var entity in entitys)
                    {
                        if (entity.side != side)
                        {
                            Shoot(entity);

                            break;
                        }
                    }
                }

                shootTimer = fireInterval;
            }
        }

        public void Shoot(Entity entity)
        {
            // 生成子弹
            var bulletTransform = Instantiate(pfbBullet);
            bulletTransform.transform.position = firePoint.position;

            var bullet          = bulletTransform.GetComponent<Bullet>();

            // 将bullet向敌人射过去

            bullet.Init(this, entity);
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
                    resourceNumeric.Add(resourceType, value);

                    break;
                }
                case ModifyNumericType.Set:
                {
                    resourceNumeric.Set(resourceType, value);

                    break;
                }
                default:
                    HMLog.LogError("未定义的ModifyNumericType");

                    break;
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        // todo 换个地方初始化吧
        private void Awake()
        {
            Init();
        }

        // 初始
        private void Init()
        {
            InitAttr();
            InitResource();
        }

        // 初始属性
        private void InitAttr()
        {
            attributesNumeric = new AttributesNumeric();
            // todo 从配置中读取
            ModifyAttribute(AttributeType.Atk, 10, ModifyNumericType.Set);
            ModifyAttribute(AttributeType.MaxHp, 200, ModifyNumericType.Set);
            ModifyAttribute(AttributeType.CriticalProbability, 0.3f, ModifyNumericType.Set);
            ModifyAttribute(AttributeType.CriticalDamage, 1.5f, ModifyNumericType.Set);
            ModifyAttribute(AttributeType.DodgeProbability, 0.1f, ModifyNumericType.Set);
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
    }
}
#pragma warning restore 0649