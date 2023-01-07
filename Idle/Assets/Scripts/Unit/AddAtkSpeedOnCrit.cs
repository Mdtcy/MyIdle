/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月7日
 * @modify date 2023年1月7日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using Event;
using IdleGame;

namespace Unit
{
    public class AddAtkSpeedOnCrit : Buff,
                                     IListenEvent<EEventOnCrit>
    {
        private Entity owener;

        public AddAtkSpeedOnCrit(Entity entity)
        {
            owener = entity;
        }

        public void Trigger(EEventOnCrit t)
        {
            var addAtkSpeed = new Add100AtkSpeedBuff(owener)
            {
                Permanent = false,
                Duration = 2f,
            };
            owener.BuffComponent.AddBuff(addAtkSpeed);
        }

        public override string Id()
        {
            return "AddAtkSpeedOnCrit";
        }
    }
}
#pragma warning restore 0649