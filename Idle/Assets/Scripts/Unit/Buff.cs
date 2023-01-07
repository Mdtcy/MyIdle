/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

using System;
using UnityEngine;

#pragma warning disable 0649

namespace IdleGame
{
    public abstract class Buff : IComparable<Buff>
    {
        public abstract string Id();

        public string Tag = "Buff";

        public bool Permanent = true;

        public float Duration;

        public float TickTime;

        public float TimeElapsed;

        public int Stack = 1;

        public int MaxStack = 999;

        public GameObject Caster;

        public int Priority;

        public virtual void OnOccur(int modStack)
        {

        }

        public virtual void  OnTick()
        {

        }

        public virtual void OnRemoved()
        {

        }

        public int CompareTo(Buff other)
        {
            return Priority.CompareTo(other.Priority);
        }

    }
}
#pragma warning restore 0649