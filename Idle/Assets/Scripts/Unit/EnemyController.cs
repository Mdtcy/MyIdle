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
        }

        private void FixedUpdate()
        {
            if (entity.dead)
            {
                return;
            }

            // Find the player todo
            // var tower = FindObjectOfType<TowerController>();
            var entities = FindObjectsOfType<Entity>();

            Entity target = null;
            foreach (var entity in entities)
            {
                if(entity.side == Side.Player)
                {
                    target = entity.GetComponent<Entity>();
                }
            }

            if (target == null)
            {
                return;
            }

            float distance = Vector3.Distance(target.transform.position, transform.position);

            if (distance <= 4 * GetComponent<CircleCollider2D>().radius)
            {
                shootTimer -= Time.fixedDeltaTime;
                if (entity.ControlState.canAttack &&
                    shootTimer <= 0)
                {
                    rb.velocity = Vector2.zero;

                    Attack(target);
                    shootTimer = entity.GetFireInterval();
                }
            }
            else
            {
                // Move towards the player
                Vector3 dir = target.transform.position - transform.position;
                rb.MovePosition(transform.position      + dir.normalized * moveSpeed * Time.fixedDeltaTime);
                // rb.velocity = dir.normalized * moveSpeed;
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
                                       new Damage.Damage(Mathf.CeilToInt(entity.atk)),
                                       transform.eulerAngles.z,
                                       new DamageInfoTag[] {DamageInfoTag.directDamage,}
                                      );
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649