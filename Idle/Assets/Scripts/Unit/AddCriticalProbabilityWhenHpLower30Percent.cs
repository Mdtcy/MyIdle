/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

using Event;
using Numeric;

#pragma warning disable 0649
namespace IdleGame
{
    public class AddCriticalProbabilityWhenHpLower30Percent : Buff,
                                                              IListenEvent<EEventHpLower30Percent>,
                                                              IListenEvent<EEventHpHigher30Percent>
    {
        private Entity owener;

        private float criticalProbabilityChanged;

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
            float ratio = owener.GetResource(ResourceType.Hp) / owener.GetAttribute(AttributeType.MaxHp);

            if (ratio < 0.3)
            {
                Enable();
            }
        }

        public void Trigger(EEventHpLower30Percent  t)
        {
            Enable();
        }

        public void Trigger(EEventHpHigher30Percent t)
        {
            Disable();
        }

        private void Enable()
        {
            owener.ModifyAttribute(AttributeType.CriticalProbability,
                                   -0.3f * Stack - criticalProbabilityChanged,
                                   ModifyNumericType.Add);
        }

        private void Disable()
        {
            owener.ModifyAttribute(AttributeType.CriticalProbability,
                                   -criticalProbabilityChanged,
                                   ModifyNumericType.Add);
        }
    }
}
#pragma warning restore 0649