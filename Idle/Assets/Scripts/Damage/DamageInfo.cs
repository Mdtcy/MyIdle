/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using System.Collections.Generic;
using IdleGame.Buff;
using UnityEngine;

namespace Damage
{
    public class DamageInfo
    {
        /// <summary>
        /// 造成伤害的攻击者，当然可以是null的
        /// </summary>
        public GameObject attacker;

        /// <summary>
        /// 造成攻击伤害的受击者，这个必须有
        /// </summary>
        public GameObject defender;

        ///<summary>
        ///这次伤害的类型Tag，这个会被用于buff相关的逻辑，是一个极其重要的信息
        ///这里是策划根据游戏设计来定义的，比如游戏中可能存在"frozen" "fire"之类的伤害类型，还会存在"directDamage" "period" "reflect"之类的类型伤害
        ///根据这些伤害类型，逻辑处理可能会有所不同，典型的比如"reflect"，来自反伤的，那本身一个buff的作用就是受到伤害的时候反弹伤害，如果双方都有这个buff
        ///并且这个buff没有判断damageInfo.tags里面有reflect，则可能造成“短路”，最终有一下有一方就秒了。
        ///</summary>
        public DamageInfoTag[] tags;

        ///<summary>
        ///伤害值，其实伤害值是多元的，通常游戏都会有多个属性伤害，所以会用一个struct，否则就会是一个int
        ///尽管起名叫Damage，但实际上治疗也是这个，只是负数叫做治疗量，这个起名看似不严谨，对于游戏（这个特殊的业务）而言却又是严谨的
        ///</summary>
        public Damage damage;

        ///<summary>
        ///是否命中，是否命中与是否暴击并不直接相关，都是单独的算法
        ///作为一个射击游戏，子弹命中敌人是一种技巧，所以在这里设计命中了还会miss是愚蠢的
        ///因此这里的hitRate始终是2，就是必定命中的，之所以把这个属性放着，也是为了说明问题，而不是这个属性真的对这个游戏有用。
        ///要不要这个属性还是取决于游戏设计，比如当前游戏，本不该有这个属性。
        ///</summary>
        public float hitRate;

        ///<summary>
        ///伤害的角度，作为伤害打向角色的入射角度，比如子弹，就是它当前的飞行角度
        ///</summary>
        public float degree;

        ///<summary>
        ///伤害过后，给角色添加的buff
        ///</summary>
        public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>();

        public DamageInfo(GameObject      attacker,
                          GameObject      defender,
                          Damage          damage,
                          float           damageDegree,
                          DamageInfoTag[] tags)
        {
            this.attacker = attacker;
            this.defender = defender;
            this.damage   = damage;
            this.degree   = damageDegree;
            this.tags     = new DamageInfoTag[tags.Length];

            for (int i = 0; i < tags.Length; i++)
            {
                this.tags[i] = tags[i];
            }
        }

        // ///<summary>
        // ///从策划脚本获得最终的伤害值 todo 换个位置
        // ///</summary>
        // public int DamageValue(bool asHeal)
        // {
        //     var   attckEntity     = attacker.GetComponent<Entity>();
        //     float critProbability = attckEntity.GetAttribute(AttributeType.CriticalProbability);
        //     float critDamage = attckEntity.GetAttribute(AttributeType.CriticalDamage);
        //
        //     bool isCrit = Random.Range(0.00f, 1.00f) <= critProbability;
        //
        //     if (!asHeal)
        //     {
        //         return Mathf.CeilToInt(damage.Overall(asHeal) *
        //                                (isCrit == true ? critDamage : 1.00f));
        //     }
        //     else
        //     {
        //         // todo 治疗不暴击
        //         return Mathf.CeilToInt(damage.Overall(asHeal));
        //     }
        // }

        ///<summary>
        ///根据tag判断，这是否是一次治疗，那些tag算是治疗，当然是策划定义了才算数的
        ///</summary>
        public bool isHeal()
        {
            for (int i = 0; i < this.tags.Length; i++)
            {
                if (tags[i] == DamageInfoTag.directHeal || tags[i] == DamageInfoTag.periodHeal)
                {
                    return true;
                }
            }

            return false;
        }

        ///<summary>
        ///根据tag决定是否要播放受伤动作，当然你还可以是根据类型决定不同的受伤动作，但是我这个demo就没这么复杂了
        ///</summary>
        public bool requireDoHurt()
        {
            for (int i = 0; i < this.tags.Length; i++)
            {
                if (tags[i] == DamageInfoTag.directDamage)
                {
                    return true;
                }
            }

            return false;
        }

        ///<summary>
        ///将添加buff信息添加到伤害信息中来
        ///buffOnHit\buffBeHurt\buffOnKill\buffBeKilled等伤害流程张的buff添加通常走这里
        ///<param name="info">要添加的buff的信息</param>
        ///</summary>
        public void AddBuffToCha(AddBuffInfo buffInfo)
        {
            this.addBuffs.Add(buffInfo);
        }
    }

    ///<summary>
    ///伤害类型的Tag元素，因为DamageInfo的逻辑需要的严谨性远高于其他的元素，所以伤害类型应该是枚举数组的
    ///这个伤害类型不应该是类似 火伤害、水伤害、毒伤害之类的，如果是这种元素伤害，那么应该是在damage做文章，即damange不是int而是一个struct或者array或者dictionary，然后DamageValue函数里面去改最终值算法
    ///这里的伤害类型，指的还是比如直接伤害、反弹伤害、dot伤害等等，一些在逻辑处理流程会有不同待遇的东西，比如dot伤害可能不会触发一些效果等，当然这最终还是取决于策划设计的规则。
    ///</summary>
    public enum DamageInfoTag
    {
        directDamage  = 0,   //直接伤害
        // periodDamage  = 1,   //间歇性伤害
        // reflectDamage = 2,   //反噬伤害
        poisonDamage  = 3,   //中毒伤害
        directHeal    = 10,  //直接治疗
        periodHeal    = 11,  //间歇性治疗
        monkeyDamage  = 9999 //这个类型的伤害在目前这个demo中没有意义，只是告诉你可以随意扩展，仅仅比string严肃些。
    }

    /// <summary>
    /// 游戏中伤害值的struct，这游戏的伤害类型包括子弹伤害（治疗）、爆破伤害（治疗）、精神伤害（治疗）3种，这两种的概念更像是类似物理伤害、金木水火土属性伤害等等这种元素伤害的概念
    /// 但是游戏的逻辑可能会依赖于这个伤害做一些文章，比如“受到子弹伤害减少90%”之类的
    /// </summary>
    public struct Damage
    {
        public int bullet;
        public int explosion;

        public Damage(int bullet, int explosion = 0)
        {
            this.bullet    = bullet;
            this.explosion = explosion;
        }

        ///<summary>
        ///统计规则，在这个游戏里伤害和治疗不能共存在一个结果里，作为抵消用
        ///<param name="asHeal">是否当做治疗来统计</name>
        ///</summary>
        public int Overall(bool asHeal = false)
        {
            return (asHeal == false)
                ? Mathf.Max(0, bullet) + Mathf.Max(0, explosion)
                : Mathf.Min(0, bullet) + Mathf.Min(0, explosion);
        }

        public static Damage operator +(Damage a, Damage b)
        {
            return new Damage(
                              a.bullet    + b.bullet,
                              a.explosion + b.explosion
                             );
        }

        public static Damage operator *(Damage a, float b)
        {
            return new Damage(
                              Mathf.RoundToInt(a.bullet    * b),
                              Mathf.RoundToInt(a.explosion * b)
                             );
        }
    }
}
#pragma warning restore 0649