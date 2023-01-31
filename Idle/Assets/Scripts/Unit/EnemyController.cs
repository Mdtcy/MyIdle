/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月27日
 * @modify date 2023年1月27日
 * @desc [EnemyController]
 */

#pragma warning disable 0649
using Damage;
using DefaultNamespace;
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
        private EntityConfig entityConfig;

        [SerializeField]
        private Entity entity;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private float moveSpeed = 5;

        // local
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
            entity.Init(entityConfig);
        }

        private void FixedUpdate()
        {
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
                    shootTimer = entity.GetFireInterval();
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
            // todo 攻击动画
            animator.SetTrigger("attack");
            // 实际攻击
            SceneVariants.CreateDamage(gameObject,
                                       target.gameObject,
                                       new Damage.Damage(Mathf.CeilToInt(entity.GetAttribute(AttributeType.Atk))),
                                       new DamageInfoTag[] {DamageInfoTag.directDamage,}
                                      );
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649