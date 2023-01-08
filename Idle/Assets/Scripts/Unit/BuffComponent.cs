/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using IdleGame.Buff;
using UnityEngine;
using UnityEngine.Pool;

namespace IdleGame
{
    public class BuffComponent : MonoBehaviour
    {
        #region FIELDS

        public List<BuffObj> Buffs = new();

        private Entity owener;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Init(Entity entity)
        {
            owener = entity;
        }

        /// <summary>
        /// 获取指定目标给施加的Buff, 意味着不同目标给的相同Buff会被分别计算
        /// </summary>
        /// <param name="id"></param>
        /// <param name="caster"></param>
        /// <returns></returns>
        public BuffObj GetBuffById(string id, GameObject caster)
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                if (Buffs[i].model.id == id &&
                    (caster == Buffs[i].caster))
                {
                    return Buffs[i];
                }
            }

            return null;
        }

        // // todo 限定一个人身上只能有一个相同的buff  不要有不同人给的相同buff
        // public Buff GetBuff(string id)
        // {
        //     foreach (var buff in Buffs)
        //     {
        //         if (id.Equals(buff.Id()))
        //         {
        //             return buff;
        //         }
        //     }
        //
        //     return null;
        // }


        // public void RemoveBuff(Buff buff)
        // {
        //     AddBuff(buff, -buff.Stack);
        // }
        //
        // public void RemoveBuff(string id)
        // {
        //     var buff = GetBuff(id);
        //     RemoveBuff(buff);
        // }

        public void AddBuff(AddBuffInfo addBuffInfo)
        {
            var     buffCaster = addBuffInfo.caster;
            BuffObj hasOne    = GetBuffById(addBuffInfo.buffModel.id, buffCaster);
            int     modStack   = addBuffInfo.addStack;
            bool    toRemove   = false;
            BuffObj toAddBuff  = null;

            if (hasOne != null)
            {
                //已经存在
                hasOne.buffParam = new Dictionary<string, object>();

                if (addBuffInfo.buffParam != null)
                {
                    foreach (KeyValuePair<string, object> kv in addBuffInfo.buffParam)
                    {
                        hasOne.buffParam[kv.Key] = kv.Value;
                    }
                }

                hasOne.duration = (addBuffInfo.durationSetTo == true)
                    ? addBuffInfo.duration
                    : (addBuffInfo.duration + hasOne.duration);
                int afterAdd = hasOne.stack + modStack;
                modStack = afterAdd >= hasOne.model.maxStack
                    ? (hasOne.model.maxStack - hasOne.stack)
                    : (afterAdd <= 0 ? (0 - hasOne.stack) : modStack);
                hasOne.stack     += modStack;
                hasOne.permanent =  addBuffInfo.permanent;
                toAddBuff            =  hasOne;
                toRemove             =  hasOne.stack <= 0;
            }
            else
            {
                //新建
                toAddBuff = new BuffObj(
                                        addBuffInfo.buffModel,
                                        addBuffInfo.caster,
                                        this.gameObject,
                                        addBuffInfo.duration,
                                        addBuffInfo.addStack,
                                        addBuffInfo.permanent,
                                        addBuffInfo.buffParam
                                       );
                Buffs.Add(toAddBuff);
                Buffs.Sort(SortBuff);
            }

            // 如果不是在移除Buff, 触发OnOccur
            if (toRemove == false)
            {
                addBuffInfo.buffModel.OnOccur(toAddBuff, modStack);
            }

            owener.ControlStateRecheck();
        }

        private int SortBuff(BuffObj x, BuffObj y)
        {
            return x.model.priority.CompareTo(y.model.priority);
        }

        // public Buff GetBuffByTag(string tag)
        // {
        //     foreach (var buff in Buffs)
        //     {
        //         if (buff.Tag.Equals(tag))
        //         {
        //             return buff;
        //         }
        //     }
        //
        //     return null;
        // }
        //
        // public void RemoveBuffByTag(string tag)
        // {
        //     for (int i = Buffs.Count - 1; i >= 0; i--)
        //     {
        //         if (Buffs[i].Tag.Equals(tag))
        //         {
        //             Buffs.RemoveAt(i);
        //             Buffs[i].OnRemoved();
        //         }
        //     }
        // }
        //
        //
        // public void RemoveBuff()
        // {
        //     // todo 直接Remove 如果此时轮询时轮询到，是否会出现错误 一个做法是先复制一份？
        // }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void FixedUpdate()
        {
            // todo 死了不应该继续
            float timePassed = Time.fixedDeltaTime;

            using (ListPool<BuffObj>.Get(out List<BuffObj> buffsToRemove))
            {
                for (int i = 0; i < Buffs.Count; i++)
                {
                    var buff = Buffs[i];

                    // 更新Buff剩余时间
                    if (buff.permanent == false)
                    {
                        buff.duration -= Time.deltaTime;
                    }

                    // 更新Buff持续时间
                    buff.timeElapsed += timePassed;

                    // 如果是固定一段时间触发的Buff 则每隔一段时间处理一次
                    if (buff.model.tickTime > 0)
                    {
                        if (buff.timeElapsed > (buff.ticked + 1) * buff.model.tickTime)
                        {
                            buff.model.OnTick(buff);
                            buff.ticked += 1;
                        }

                        // // float取模不精准，所以用x1000后的整数来
                        //     if (Mathf.RoundToInt(buff.timeElapsed    * 1000) %
                        //         Mathf.RoundToInt(buff.model.tickTime * 1000) ==
                        //         0)
                        //     {
                        //         buff.model.OnTick(buff);
                        //         buff.ticked += 1;
                        //     }
                    }

                    // 只要duration <= 0，不管是否是permanent都移除掉
                    // Stack为0也会被移除掉
                    if (buff.duration <= 0 || buff.stack <= 0)
                    {
                        buff.model.OnRemoved(buff);
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

                    owener.ControlStateRecheck();
                }
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649