/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using System.Collections.Generic;
using System.Linq;
using IdleGame;
using PopUpText;
using UnityEngine;

namespace Damage
{
    public class DamageManager : MonoBehaviour
    {
        // todo 找个中介 不要直接引用
        [SerializeField]
        private PopUpTextManager popUpTextManager;


        private List<DamageInfo> damageInfos = new();

        private void FixedUpdate()
        {
            int i = 0;

            while (i < damageInfos.Count)
            {
                DealWithDamage(damageInfos[i]);
                damageInfos.RemoveAt(0);
            }
        }

        ///<summary>
        ///处理DamageInfo的流程，也就是整个游戏的伤害流程
        ///<param name="dInfo">要处理的damageInfo</param>
        ///<retrun>处理完之后返回出一个damageInfo，依照这个，给对应角色扣血处理</return>
        ///</summary>
        private void DealWithDamage(DamageInfo dInfo)
        {
            //如果目标已经挂了，就直接return了
            if (!dInfo.defender)
            {
                return;
            }

            Entity defenderChaState = dInfo.defender.GetComponent<Entity>();

            if (!defenderChaState)
            {
                return;
            }

            if (defenderChaState.dead == true)
            {
                return;
            }

            Entity attackerChaState = null;

            // 先走一遍所有攻击者的onHit
            if (dInfo.attacker)
            {
                attackerChaState = dInfo.attacker.GetComponent<Entity>();

                for (int i = 0; i < attackerChaState.BuffComponent.Buffs.Count; i++)
                {
                    var buffObj = attackerChaState.BuffComponent.Buffs[i];
                    buffObj.model.OnHit(buffObj, ref dInfo, dInfo.defender);
                }
            }

            // 然后走一遍挨打者的beHurt
            for (int i = 0; i < defenderChaState.BuffComponent.Buffs.Count; i++)
            {
                var buffObj = defenderChaState.BuffComponent.Buffs[i];
                buffObj.model.OnBeHurt(buffObj, ref dInfo, dInfo.attacker);
            }

            // 计算实际的伤害和治疗量
            bool isHeal = dInfo.isHeal();
            int  dVal;

            var   attckEntity     = dInfo.attacker.GetComponent<Entity>();
            float critProbability = attckEntity.critChance;
            float critDamage      = attckEntity.critDamage;

            // todo 直接伤害才可以暴击
            bool isCrit = Random.Range(0.00f, 1.00f) <= critProbability &&
                          dInfo.tags.Contains(DamageInfoTag.directDamage);

            if (!isHeal)
            {
                dVal = Mathf.CeilToInt(dInfo.damage.Overall(isHeal) *
                                       (isCrit == true ? critDamage : 1.00f));
            }
            else
            {
                // todo 治疗不暴击
                dVal = Mathf.CeilToInt(dInfo.damage.Overall(isHeal));
            }

            //如果角色可能被杀死，就会走OnKill和OnBeKilled，这个游戏里面没有免死金牌之类的技能，所以只要判断一次就好
            if (defenderChaState.CanBeKilledByDamageInfo(dInfo, dVal) == true)
            {
                if (attackerChaState != null)
                {
                    for (int i = 0; i < attackerChaState.BuffComponent.Buffs.Count; i++)
                    {
                        var buffObj = attackerChaState.BuffComponent.Buffs[i];
                        buffObj.model.OnKill(buffObj, dInfo, dInfo.defender);
                    }
                }

                for (int i = 0; i < defenderChaState.BuffComponent.Buffs.Count; i++)
                {
                    var buffObj = defenderChaState.BuffComponent.Buffs[i];
                    buffObj.model.OnBeKilled(buffObj, dInfo, dInfo.attacker);
                }
            }

            if (isHeal == true || defenderChaState.ImmuneTime <= 0)
            {
                // todo 受伤效果
                // if (dInfo.requireDoHurt() == true && defenderChaState.CanBeKilledByDamageInfo(dInfo) == false)
                // {
                //     UnitAnim ua = defenderChaState.GetComponent<UnitAnim>();
                //     if (ua) ua.Play("Hurt");
                // }

                // todo 这里扣血在OnKill OnBeKilled之后 是否会出现问题
                defenderChaState.TakeDamage(dVal);
                //按游戏设计的规则跳数字，如果要有暴击，也可以丢在策划脚本函数（lua可以返回多参数）也可以随便怎么滴
                popUpTextManager.PopUpNumberOnCharacter(dInfo.defender, Mathf.Abs(dVal), isHeal, isCrit);
            }

            //伤害流程走完，添加buff
            for (int i = 0; i < dInfo.addBuffs.Count; i++)
            {
                GameObject toCha      = dInfo.addBuffs[i].target;
                Entity   toChaState = toCha.Equals(dInfo.attacker) ? attackerChaState : defenderChaState;

                if (toChaState != null && toChaState.dead == false)
                {
                    toChaState.BuffComponent.AddBuff(dInfo.addBuffs[i]);
                }
            }
        }

        ///<summary>
        ///添加一个damageInfo
        ///<param name="attacker">攻击者，可以为null</param>
        ///<param name="target">挨打对象</param>
        ///<param name="damage">基础伤害值</param>
        ///<param name="damageDegree">伤害的角度</param>
        ///<param name="criticalRate">暴击率，0-1</param>
        ///<param name="tags">伤害信息类型</param>
        ///</summary>
        public void DoDamage(GameObject      attacker,
                             GameObject      target,
                             Damage          damage,
                             float           damageDegree,
                             DamageInfoTag[] tags)
        {
            damageInfos.Add(new DamageInfo(attacker, target, damage, damageDegree, tags));
        }
    }
}
#pragma warning restore 0649