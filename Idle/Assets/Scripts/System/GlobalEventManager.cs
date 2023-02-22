/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月14日
 * @modify date 2023年2月14日
 * @desc []
 */

using Damage;
using IdleGame;
using UnityEngine;

#pragma warning disable 0649
namespace DefaultNamespace.System
{
    public class GlobalEventManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private float damageForce;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void OnHit(Entity attacker, Entity defender)
        {
            SceneVariants.CreateDamage(attacker.gameObject,
                                       defender.gameObject,
                                       new Damage.Damage((int) attacker.atk),
                                       transform.eulerAngles.z,
                                       new DamageInfoTag[] {DamageInfoTag.directDamage,}
                                      );
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