/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月28日
 * @modify date 2023年1月28日
 * @desc [PoisonBuffModel]
 */

#pragma warning disable 0649
using Damage;
using DefaultNamespace;
using Unit;

namespace IdleGame.Buff.BuffModels.PoisonSkills
{
    public class PoisonBuffModel : BuffModel
    {
        public PoisonBuffModel()
        {
            id       = "PoisonBuffModel";
            name     = "毒";
            priority = 1;
            maxStack = 999999;
            tags     = null;
            tickTime = 1;
            stateMod = ChaControlState.Orgin;
        }

        public override void OnOccur(BuffObj buff, int modifyStack)
        {
            base.OnOccur(buff, modifyStack);
            // todo 视觉效果
        }

        public override void OnRemoved(BuffObj buff)
        {
            base.OnRemoved(buff);
            // todo 视觉效果
        }

        public override void OnTick(BuffObj buff)
        {
            base.OnTick(buff);
            SceneVariants.CreateDamage(buff.caster,
                                       buff.carrier,
                                       new Damage.Damage(buff.stack),
                                       new DamageInfoTag[] {DamageInfoTag.poisonDamage,}
                                      );
            var entity = buff.caster.GetComponent<Entity>();
            entity.BuffComponent.AddBuff(new AddBuffInfo(
                                                         new PoisonBuffModel(),
                                                         buff.caster,
                                                         buff.carrier,
                                                         -1,
                                                         3f,
                                                         true,
                                                         true,
                                                         null));
        }
    }
}
#pragma warning restore 0649