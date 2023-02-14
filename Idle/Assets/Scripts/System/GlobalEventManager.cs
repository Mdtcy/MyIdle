/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月14日
 * @modify date 2023年2月14日
 * @desc [任务组UI]
 */

using Damage;
using IdleGame;
using Unit;
using UnityEngine;

#pragma warning disable 0649
namespace DefaultNamespace.System
{
    public class GlobalEventManager:MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private float damageForce;

        public static GlobalEventManager Instance;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void OnHit(DamageInfo damageInfo, GameObject hitObject)
        {
            var attacker = damageInfo.attacker.GetComponent<Entity>();
            var defender = damageInfo.defender.GetComponent<Entity>();

            if (defender != null)
            {
                var     rotation  = Quaternion.Euler(0, 0, damageInfo.degree);
                Vector2 direction = rotation * Vector2.right.normalized;
                defender.GetComponent<Rigidbody2D>().AddForce(direction*damageForce, ForceMode2D.Impulse);

                // defender.GetComponent<TopDownController2D>().Impact(direction, damageForce);
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnEnable()
        {
            if (Instance != null)
            {
                Debug.LogError("Only one GlobalEventManager can exist at a time.");

                return;
            }

            Instance = this;
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649