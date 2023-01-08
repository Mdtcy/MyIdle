/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using Damage;
using DefaultNamespace;
using Unit;

namespace IdleGame.Buff.BuffModels
{
    public class HealOverTimePoint3 : BuffModel
    {
        public HealOverTimePoint3()
        {
            id       = "HealOverTimePoint3";
            name     = "每0.3秒回一次血";
            priority = 1;
            maxStack = 1000;
            tags     = null;
            tickTime = 1f;
            stateMod = ChaControlState.Orgin;
        }

        public override void OnTick(BuffObj buff)
        {
            base.OnTick(buff);
            SceneVariants.CreateDamage(buff.carrier,
                                       buff.carrier,
                                       new Damage.Damage(-buff.stack),
                                       new DamageInfoTag[] {DamageInfoTag.directHeal,}
                                      );
            // buff.carrier.GetComponent<Entity>().ModifyResource(ResourceType.Hp, buff.stack, ModifyNumericType.Add);
        }
    }
}
#pragma warning restore 0649