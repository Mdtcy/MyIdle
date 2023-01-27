/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月27日
 * @modify date 2023年1月27日
 * @desc [EnemyController]
 */

#pragma warning disable 0649
using System;
using Damage;
using DefaultNamespace;
using HM;
using IdleGame;
using Numeric;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Unit
{
    public class EnemyController : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Entity entity;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private float moveSpeed = 5;


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
            entity.Init();
            // ResetShootTimer();
        }

        private void FixedUpdate()
        {
            // return;

            if (entity.dead)
            {
                return;
            }

            // Find the player todo
            var tower = FindObjectOfType<TowerController>();

            float distance = Vector3.Distance(tower.transform.position, transform.position);

            if (distance <= 4 * GetComponent<CircleCollider2D>().radius)
            {
                shootTimer -= Time.fixedDeltaTime;
                if (entity.ControlState.canAttack &&
                    shootTimer <= 0)
                {
                    rb.velocity = Vector2.zero;

                    Attack(tower.Entity);
                    ResetShootTimer();
                }
            }
            else
            {
                // Move towards the player
                Vector3 dir = tower.transform.position - transform.position;
                rb.velocity = dir.normalized * moveSpeed;
            }
        }

        [Button]
        private void Attack(Entity target)
        {
            HMLog.LogDebug("Attack");
            // todo 攻击动画
            animator.SetTrigger("attack");
            // 实际攻击
            SceneVariants.CreateDamage(gameObject,
                                       target.gameObject,
                                       new Damage.Damage(Mathf.CeilToInt(entity.GetAttribute(AttributeType.Atk))),
                                       new DamageInfoTag[] {DamageInfoTag.directDamage,}
                                      );
        }

        private void ResetShootTimer()
                {
                    float attackSpeed      = entity.GetAttribute(AttributeType.AttackSpeed);
                    float baseFireInterval = entity.GetAttribute(AttributeType.BaseFireInterval);

                    // todo 每次攻击的时间 = BAT / [(初始攻击速度 + IAS) × 0.01] = 1 / (每秒攻击的次数) 100 是基础攻速
                    // todo https://dota2.fandom.com/zh/wiki/%E6%94%BB%E5%87%BB%E9%80%9F%E5%BA%A6?variant=zh
                    float fireInterval = baseFireInterval / ((attackSpeed + 100) * 0.01f);
                    shootTimer = fireInterval;
                }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649