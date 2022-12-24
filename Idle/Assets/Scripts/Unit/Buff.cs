/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649

namespace Test
{
    public class Buff
    {
        public string Tag = "Buff";

        public bool Permanent = true;

        public float Duration;

        public float TickTime;

        public float TimeElapsed;

        public int Stack = 1;

        public virtual void  OnTick()
        {

        }

        public virtual void OnAdd()
        {

        }

        public virtual void OnRemoved()
        {

        }
    }
}
#pragma warning restore 0649