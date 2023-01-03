/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

using Event;
using UnityEngine;

#pragma warning disable 0649
namespace IdleGame
{
    public class AddCriticalProbabilityWhenHpLower30Percent : Buff,
                                                              IListenEvent<EEventHpLower30Percent>,
                                                              IListenEvent<EEventHpHigher30Percent>
    {

        private Entity owener;

        public AddCriticalProbabilityWhenHpLower30Percent(Entity entity)
        {
            owener = entity;
        }

        public override string Id()
        {
            return "AddCriticalProbabilityWhenHpLower30";
        }

        public override void OnOccur(int modStack)
        {
            // if (Stack == 1 && modStack == 1)
            // {
            //     Debug.Log("OnAdd");
            //
            //     if (owener.hp / owener.maxHp >= 0.3f)
            //     {
            //         owener.BuffComponent.RemoveBuffByTag("AddCriticalProbabilityWhenHpLower30");
            //     }
            //     else
            //     {
            //         if (owener.BuffComponent.GetBuffByTag("AddCriticalProbabilityWhenHpLower30") == null)
            //         {
            //             var buff = new AddCriticalProbability
            //             {
            //                 Tag = "AddCriticalProbabilityWhenHpLower30"
            //             };
            //             owener.BuffComponent.AddBuff(buff);
            //         }
            //     }
            // }
        }


        // private void OnHpUpdated(OnHpUpdatedSignal signal)
        // {
        //     if (signal.Entity == owener)
        //     {
        //         if (owener.hp / owener.maxHp >= 0.3f)
        //         {
        //             var buff = owener.BuffComponent.GetBuff("AddCriticalProbability");
        //
        //             // owener.BuffComponent.RemoveBuffByTag("AddCriticalProbabilityWhenHpLower30");
        //             owener.BuffComponent.RemoveBuff("AddCriticalProbability");
        //         }
        //         else
        //         {
        //             if (owener.BuffComponent.GetBuff("AddCriticalProbability") == null)
        //             {
        //                 var buff = new AddCriticalProbability
        //                 {
        //                     Tag = "AddCriticalProbabilityWhenHpLower30",
        //                     Permanent = true
        //                 };
        //                 owener.BuffComponent.AddBuff(buff);
        //             }
        //         }
        //     }
        // }

        public void Trigger(EEventHpLower30Percent  t)
        {
            Debug.Log("<30");

        }

        public void Trigger(EEventHpHigher30Percent t)
        {
            Debug.Log(">IEventHpHigher30Percent");

        }
    }
}
#pragma warning restore 0649