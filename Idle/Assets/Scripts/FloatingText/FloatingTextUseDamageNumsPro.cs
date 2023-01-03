/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月3日
 * @modify date 2023年1月3日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using DamageNumbersPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.FloatingText
{
    public class FloatingTextUseDamageNumsPro : MonoBehaviour, IFloatingText
    {
        #region FIELDS

        [LabelText("伤害数字")]
        [SerializeField]
        private DamageNumber damageNumber;

        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS

        public void SpawnDamageNum(Vector3 pos, string text)
        {
            damageNumber.Spawn(pos, text);
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