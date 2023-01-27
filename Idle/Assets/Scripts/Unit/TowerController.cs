/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using IdleGame;
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
            shootTimer = Entity.GetFireInterval();
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

                shootTimer = Entity.GetFireInterval();
            }
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