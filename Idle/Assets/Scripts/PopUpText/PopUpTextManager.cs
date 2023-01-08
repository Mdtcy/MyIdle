/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using DamageNumbersPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PopUpText
{
    public class PopUpTextManager : MonoBehaviour
    {
        [BoxGroup("Floating Text")]
        [SerializeField]
        private DamageNumber normalDamage;

        [BoxGroup("Floating Text")]
        [SerializeField]
        private DamageNumber criticalDamage;

        [BoxGroup("Floating Text")]
        [SerializeField]
        private DamageNumber healDamage;

        // [BoxGroup("Floating Text")]
        // [SerializeField]
        // private DamageNumber missDamage;

        public void PopUpNumberOnCharacter(GameObject cha, int value, bool asHeal = false, bool asCritical = false)
        {
            if (asHeal)
            {
                healDamage.Spawn(cha.transform.position + new Vector3(0, 0.6f, 0), $"+{value}");
            }
            else if (asCritical)
            {
                criticalDamage.Spawn(cha.transform.position + new Vector3(0, 0.6f, 0), $"-{value}!");
            }
            else
            {
                normalDamage.Spawn(cha.transform.position + new Vector3(0, 0.5f, 0), $"-{value}");
            }
        }
    }
}
#pragma warning restore 0649