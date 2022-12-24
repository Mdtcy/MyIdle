/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

using Zenject;

#pragma warning disable 0649
namespace Test
{
    public class AddCriticalProbabilityWhenHpLower30 : Buff
    {
        [Inject]
        private SignalBus signalBus;

        private Entity owener;

        public AddCriticalProbabilityWhenHpLower30(Entity entity)
        {
            owener = entity;
        }

        public override void OnAdd()
        {
            signalBus.Subscribe<OnHpUpdatedSignal>(OnHpUpdated);

            if (owener.hp / owener.maxHp >= 0.3f)
            {
                owener.BuffComponent.RemoveBuffByTag("AddCriticalProbabilityWhenHpLower30");
            }
            else
            {
                var ss = owener.BuffComponent.GetBuffByTag("AddCriticalProbabilityWhenHpLower30");
                if (ss == null)
                {
                    var buff = new AddCriticalProbability
                    {
                        Tag = "AddCriticalProbabilityWhenHpLower30"
                    };
                    owener.BuffComponent.AddBuff(buff);
                }
            }
        }

        public override void OnRemoved()
        {
            signalBus.Unsubscribe<OnHpUpdatedSignal>(OnHpUpdated);
        }

        private void OnHpUpdated(OnHpUpdatedSignal signal)
        {
            if (signal.Entity == owener)
            {
                if (owener.hp / owener.maxHp >= 0.3f)
                {
                    owener.BuffComponent.RemoveBuffByTag("AddCriticalProbabilityWhenHpLower30");
                }
                else
                {
                    if (owener.BuffComponent.GetBuffByTag("AddCriticalProbabilityWhenHpLower30") == null)
                    {
                        var buff = new AddCriticalProbability
                        {
                            Tag = "AddCriticalProbabilityWhenHpLower30",
                            Permanent = true
                        };
                        owener.BuffComponent.AddBuff(buff);
                    }
                }
            }
        }
    }
}
#pragma warning restore 0649