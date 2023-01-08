/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using IdleGame;
using Numeric;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Unit
{
    public class TowerController : MonoBehaviour
    {
        #region FIELDS

        public Entity Entity;

        [SerializeField]
        private Transform pfbBullet;

        [ShowInInspector]
        [ReadOnly]
        private float shootTimer;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            Entity.Init();
            ResetShootTimer();
        }

        private void FixedUpdate()
        {
            if (!Entity.ControlState.canAttack)
            {
                return;
            }

            shootTimer -= Time.fixedDeltaTime;

            while (shootTimer <= 0)
            {
                var entitys = FindObjectsOfType<Entity>();

                if (entitys.Length > 0)
                {
                    foreach (var entity in entitys)
                    {
                        if (entity.side != Entity.side)
                        {
                            Shoot(entity);

                            break;
                        }
                    }
                }

                ResetShootTimer();
            }
        }

        private void ResetShootTimer()
        {
            float attackSpeed      = Entity.GetAttribute(AttributeType.AttackSpeed);
            float baseFireInterval = Entity.GetAttribute(AttributeType.BaseFireInterval);

            // todo 每次攻击的时间 = BAT / [(初始攻击速度 + IAS) × 0.01] = 1 / (每秒攻击的次数) 100 是基础攻速
            // todo https://dota2.fandom.com/zh/wiki/%E6%94%BB%E5%87%BB%E9%80%9F%E5%BA%A6?variant=zh
            float fireInterval = baseFireInterval / ((attackSpeed + 100) * 0.01f);
            shootTimer = fireInterval;
        }

        private void Shoot(Entity entity)
        {
            // 生成子弹
            var bulletTransform = Instantiate(pfbBullet);
            bulletTransform.transform.position = transform.position;

            var bullet = bulletTransform.GetComponent<Bullet>();

            // 将bullet向敌人射过去

            bullet.Init(Entity, entity);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649