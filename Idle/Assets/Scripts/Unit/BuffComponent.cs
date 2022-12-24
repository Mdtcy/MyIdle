/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Test
{
    public class BuffComponent : MonoBehaviour
    {
        #region FIELDS

        public List<Buff> Buffs = new List<Buff>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void AddBuff(Buff buff)
        {
            Buffs.Add(buff);
            buff.OnAdd();
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

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void FixedUpdate()
        {
            float timePassed = Time.fixedDeltaTime;

            using (ListPool<Buff>.Get(out List<Buff> buffsToRemove))
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
                        buffsToRemove.Add(buff);
                    }
                }

                // 移除Buff
                if (buffsToRemove.Count > 0)
                {
                    foreach (var buffToRemove in buffsToRemove)
                    {
                        buffToRemove.OnRemoved();
                        Buffs.Remove(buffToRemove);
                    }

                    AttrRecheck();
                }
            }
        }

        // 计算属性
        private void AttrRecheck()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649