/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using Event;
using HM.Extensions;
using UnityEngine;
using UnityEngine.Pool;

namespace IdleGame
{
    public class BuffComponent : MonoBehaviour
    {
        #region FIELDS

        public List<Buff> Buffs = new();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public Buff GetBuff(string id, GameObject caster)
        {
            foreach (var buff in Buffs)
            {
                if (id.Equals(buff.Id()) && caster == buff.Caster)
                {
                    return buff;
                }
            }

            return null;
        }

        // todo 限定一个人身上只能有一个相同的buff  不要有不同人给的相同buff
        public Buff GetBuff(string id)
        {
            foreach (var buff in Buffs)
            {
                if (id.Equals(buff.Id()))
                {
                    return buff;
                }
            }

            return null;
        }


        public void RemoveBuff(Buff buff)
        {
            AddBuff(buff, -buff.Stack);
        }

        public void RemoveBuff(string id)
        {
            var buff = GetBuff(id);
            RemoveBuff(buff);
        }

        /// <summary>
        /// 为角色添加buff，当然，删除也是走这个的
        /// </summary>
        public void AddBuff(Buff buff, int stack = 1, bool durationSetTo = true)
        {
            Buff hasOne    = GetBuff(buff.Id(), buff.Caster);
            int  modStack  = stack;
            bool toRemove  = false;
            Buff toAddBuff = null;

            if (hasOne != null)
            {
                // 如果不是永久的Buff,需要更新持续时间
                if (!hasOne.Permanent)
                {
                    hasOne.Duration = durationSetTo ? buff.Duration : (buff.Duration + hasOne.Duration);
                }

                int afterAdd = hasOne.Stack + modStack;
                modStack = afterAdd >= hasOne.MaxStack
                    ? (hasOne.MaxStack - hasOne.Stack)
                    : (afterAdd <= 0 ? (0 - hasOne.Stack) : modStack);
                hasOne.Stack     += modStack;
                hasOne.Permanent =  buff.Permanent;
                toAddBuff        =  hasOne;
                toRemove         =  hasOne.Stack <= 0;
            }
            else
            {
                //新建
                toAddBuff = buff;
                Buffs.Add(toAddBuff);
                Buffs.Sort();
            }

            if (toRemove == false)
            {
                toAddBuff.OnOccur(modStack);
            }

            AttrRecheck();
        }

        public Buff GetBuffByTag(string tag)
        {
            foreach (var buff in Buffs)
            {
                if (buff.Tag.Equals(tag))
                {
                    return buff;
                }
            }

            return null;
        }

        public void RemoveBuffByTag(string tag)
        {
            for (int i = Buffs.Count - 1; i >= 0; i--)
            {
                if (Buffs[i].Tag.Equals(tag))
                {
                    Buffs.RemoveAt(i);
                    Buffs[i].OnRemoved();
                }
            }
        }


        public void RemoveBuff()
        {
            // todo 直接Remove 如果此时轮询时轮询到，是否会出现错误 一个做法是先复制一份？
        }

        public void TriggerEvent<T>(T tEvent) where T : Event.EEvent
        {
            var list = HM.GameBase.ListPool<Buff>.Claim();
            list.AddRange(Buffs);
            foreach (var buff in list)
            {
                if (buff is IListenEvent<T> t)
                {
                    t.Trigger(tEvent);
                }
            }

            list.ReleaseToPool();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void FixedUpdate()
        {
            float timePassed = Time.fixedDeltaTime;

            using ( ListPool<Buff>.Get(out List<Buff> buffsToRemove))
            {
                for (int i = 0; i < Buffs.Count; i++)
                {

                    var buff = Buffs[i];

                    // 更新Buff剩余时间
                    if (buff.Permanent == false)
                    {
                        buff.Duration -= Time.deltaTime;
                    }

                    // 更新Buff持续时间
                    buff.TimeElapsed += timePassed;

                    // 如果是固定一段时间触发的Buff 则每隔一段时间处理一次
                    if (buff.TickTime > 0)
                    {
                        // float取模不精准，所以用x1000后的整数来
                        if (Mathf.RoundToInt(buff.TimeElapsed * 1000) %
                            Mathf.RoundToInt(buff.TickTime    * 1000) ==
                            0)
                        {
                            buff.OnTick();
                        }
                    }

                    // 只要duration <= 0，不管是否是permanent都移除掉
                    // Stack为0也会被移除掉
                    if (buff.Duration <= 0 || buff.Stack <= 0)
                    {
                        buff.OnRemoved();
                        buffsToRemove.Add(buff);
                    }
                }

                // 移除Buff
                if (buffsToRemove.Count > 0)
                {
                    foreach (var buffToRemove in buffsToRemove)
                    {
                        Buffs.Remove(buffToRemove);
                    }

                    AttrRecheck();
                }
            }
        }

        // 计算属性
        private void AttrRecheck()
        {
            // todo
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649