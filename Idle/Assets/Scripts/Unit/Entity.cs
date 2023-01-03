/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using Event;
using IdleGame;
using Test;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IdleGame
{
    public class Entity : MonoBehaviour
    {
        #region FIELDS

        public string name;

        public int    attack;

        public float maxHp;

        public float hp;

        public float criticalProbability;

        public float criticalDamageRatio;

        public float dodgeProbability;

        public Side side;


        public BuffComponent BuffComponent;


        public float testC;
        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Attack(Entity other)
        {
            // 闪避判断
            if (Random.Range(0f, 1f) <= dodgeProbability)
            {
                other.OnMiss();
            }
            // 命中敌人
            else
            {
                float damage = attack;

                if (Random.Range(0f, 1f) <= criticalProbability)
                {
                    damage *= criticalDamageRatio;
                }

                other.OnHurt(damage);
            }
        }

        public void OnHurt(float damage)
        {
            if (damage >= hp)
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
            BeforeChangeHp(changed);
            hp += changed;
        }


        private void BeforeChangeHp(float changed)
        {
            if (hp / maxHp < 0.3f && (hp + changed)/ maxHp >= 0.3f)
            {
                BuffComponent.TriggerEvent(new EEventHpHigher30Percent());
            }
            else if (hp / maxHp >= 0.3f && (hp + changed) / maxHp < 0.3f)
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