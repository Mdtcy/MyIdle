/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

using Damage;
using Unit;
using UnityEngine;

#pragma warning disable 0649
namespace IdleGame.Buff
{
    public class BuffModel
    {
        ///<summary>
        ///buff的id
        ///</summary>
        public string id;

        ///<summary>
        ///buff的名称
        ///</summary>
        public string name;

        ///<summary>
        ///buff的优先级，优先级越低的buff越后面执行，这是一个非常重要的属性
        ///比如经典的“吸收50点伤害”和“受到的伤害100%反弹给攻击者”应该反弹多少，取决于这两个buff的priority谁更高
        ///</summary>
        public int priority;

        ///<summary>
        ///buff堆叠的规则中需要的层数，在这个游戏里只要id和caster相同的buffObj就可以堆叠
        ///激战2里就不同，尽管图标显示堆叠，其实只是统计了有多少个相同id的buffObj作为层数显示了
        ///</summary>
        public int maxStack;

        ///<summary>
        ///buff的tag
        ///</summary>
        public string[] tags;

        ///<summary>
        ///buff的工作周期，单位：秒。
        ///每多少秒执行工作一次，如果<=0则代表不会周期性工作，只要>0，则最小值为Time.FixedDeltaTime。
        ///</summary>
        public float tickTime;

        // ///<summary>
        // ///buff会给角色添加的属性，这些属性根据这个游戏设计只有2种，plus和times，所以这个数组实际上只有2维
        // ///</summary>
        // public ChaProperty[] propMod;

        // /// <summary>
        // /// buff会给角色添加的属性
        // /// </summary>
        // public AttributesNumeric AttributesNumeric;

        ///<summary>
        ///buff对于角色的ChaControlState的影响
        ///</summary>
        public ChaControlState stateMod = ChaControlState.Orgin;

        ///<summary>
        ///buff在被添加、改变层数时候触发的事件
        ///<param name="buff">会传递给脚本buffObj作为参数</param>
        ///<param name="modifyStack">会传递本次改变的层数</param>
        ///</summary>
        public virtual void OnOccur(BuffObj buff, int modifyStack)
        {

        }

        ///<summary>
        ///buff在每个工作周期会执行的函数，如果这个函数为空，或者tickTime<=0，都不会发生周期性工作
        ///<param name="buff">会传递给脚本buffObj作为参数</param>
        ///</summary>
        public virtual void OnTick(BuffObj buff)
        {

        }

        ///<summary>
        ///在这个buffObj被移除之前要做的事情，如果运行之后buffObj又不足以被删除了就会被保留
        ///<param name="buff">会传递给脚本buffObj作为参数</param>
        ///</summary>
        public virtual void OnRemoved(BuffObj buff)
        {

        }

        // ///<summary>
        // ///在释放技能的时候运行的buff，执行这个buff获得最终技能要产生的Timeline
        // ///<param name="buff">会传递给脚本的buffObj</param>
        // ///<param name="skill">即将释放的技能skillObj</param>
        // ///<param name="timeline">释放出来的技能，也就是一个timeline，这里的本质就是让你通过buff还能对timeline进行hack以达到修改技能效果的目的</return>
        // ///</summary>
        // public virtual TimelineObj OnCast(BuffObj buff, SkillObj skill, TimelineObj timeline)
        // {
        //
        // }

        ///<summary>
        ///在伤害流程中，持有这个buff的人作为攻击者会发生的事情
        ///<param name="buff">会传递给脚本buffObj作为参数</param>
        ///<param name="damageInfo">这次的伤害信息</param>
        ///<param name="target">挨打的角色对象</param>
        ///</summary>
        public virtual void OnHit(BuffObj buff, ref DamageInfo damageInfo, GameObject target)
        {

        }

        ///<summary>
        ///在伤害流程中，持有这个buff的人作为挨打者会发生的事情
        ///<param name="buff">会传递给脚本buffObj作为参数</param>
        ///<param name="damageInfo">这次的伤害信息</param>
        ///<param name="attacker">打我的角色，当然可以是空的</param>
        ///</summary>
        public virtual void OnBeHurt(BuffObj buff, ref DamageInfo damageInfo, GameObject attacker)
        {

        }

        ///<summary>
        ///在伤害流程中，如果击杀目标，则会触发的啥事情
        ///<param name="buff">会传递给脚本buffObj作为参数</param>
        ///<param name="damageInfo">这次的伤害信息</param>
        ///<param name="target">挨打的角色对象</param>
        ///</summary>
        public virtual void OnKill(BuffObj buff, DamageInfo damageInfo, GameObject target)
        {

        }

        ///<summary>
        ///在伤害流程中，持有这个buff的人被杀死了，会触发的事情
        ///<param name="buff">会传递给脚本buffObj作为参数</param>
        ///<param name="damageInfo">这次的伤害信息</param>
        ///<param name="attacker">发起攻击造成击杀的角色对象</param>
        ///</summary>
        public virtual void OnBeKilled(BuffObj buff, DamageInfo damageInfo, GameObject attacker)
        {

        }
    }
}
#pragma warning restore 0649