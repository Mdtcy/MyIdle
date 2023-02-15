/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using Damage;
using DefaultNamespace.Game;
using UnityEngine;

namespace DefaultNamespace
{
    public class SceneVariants
    {
        ///<summary>
        ///添加一个damageInfo
        ///<param name="attacker">攻击者，可以为null</param>
        ///<param name="target">挨打对象</param>
        ///<param name="damage">基础伤害值</param>
        ///<param name="damageDegree">伤害的角度</param>
        ///<param name="criticalRate">暴击率，0-1</param>
        ///<param name="tags">伤害信息类型</param>
        ///</summary>
        public static void CreateDamage(GameObject      attacker,
                                        GameObject      target,
                                        Damage.Damage   damage,
                                        float damageDegree,
                                        DamageInfoTag[] tags)
        {
            GameManager.Instance.DamageManager.DoDamage(attacker, target, damage, damageDegree, tags);
        }
    }
}
#pragma warning restore 0649