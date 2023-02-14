/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

namespace IdleGame.Buff
{
    public class BuffObj
    {
        ///<summary>
        ///这是个什么buff
        ///</summary>
        public BuffConfig model;

        ///<summary>
        ///剩余多久，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///是否是一个永久的buff，永久的duration不会减少，但是timeElapsed还会增加
        ///</summary>
        public bool permanent;

        ///<summary>
        ///当前层数
        ///</summary>
        public int stack;

        ///<summary>
        ///buff的施法者是谁，当然可以是空的
        ///</summary>
        public GameObject caster;

        ///<summary>
        ///buff的携带者，实际上是作为参数传递给脚本用，具体是谁，可定是所在控件的this.gameObject了
        ///</summary>
        public GameObject carrier;

        ///<summary>
        ///buff已经存在了多少时间了，单位：秒
        ///</summary>
        public float timeElapsed = 0.00f;

        ///<summary>
        ///buff的一些参数，这些参数是逻辑使用的，比如wow中牧师的盾还能吸收多少伤害，就可以记录在buffParam里面
        ///</summary>
        public Dictionary<string, object> buffParam = new Dictionary<string, object>();

        public BuffObj(BuffConfig                  model,
                       GameObject                 caster,
                       GameObject                 carrier,
                       float                      duration,
                       int                        stack,
                       bool                       permanent = false,
                       Dictionary<string, object> buffParam = null)
        {
            this.model     = model;
            this.caster    = caster;
            this.carrier   = carrier;
            this.duration  = duration;
            this.stack     = stack;
            this.permanent = permanent;

            if (buffParam != null)
            {
                foreach (KeyValuePair<string, object> kv in buffParam)
                {
                    this.buffParam.Add(kv.Key, kv.Value);
                }
            }
        }
    }
}
#pragma warning restore 0649