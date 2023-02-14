/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月29日
 * @modify date 2022年12月29日
 * @desc []
 */

#pragma warning disable 0649
using Damage;
using DefaultNamespace;
using DefaultNamespace.System;
using UnityEngine;

namespace IdleGame
{
    public class TrackBullet : MonoBehaviour
    {
        #region FIELDS

        private Entity caster;
        private Entity target;

        public Rigidbody2D Rb2D;

        public float speed;

        public float rotateSpeed;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Init(Entity caster, Entity target)
        {
            this.caster = caster;
            this.target = target;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            // dir
            Vector3    vectorToTarget = target.transform.position - transform.position;
            float      angle          = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q              = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

            // move
            Vector3 dir = transform.TransformDirection(Vector3.right);
            Rb2D.velocity = dir * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var entity = other.GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }

            if (entity.side != caster.side)
            {

                SceneVariants.CreateDamage(caster.gameObject,
                                           target.gameObject,
                                           new Damage.Damage((int)caster.atk),
                                           transform.eulerAngles.z,
                                           new DamageInfoTag[] {DamageInfoTag.directDamage,}
                                          );

                Destroy(gameObject);
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649