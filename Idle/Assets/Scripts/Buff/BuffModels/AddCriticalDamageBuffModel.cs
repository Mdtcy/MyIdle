/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using Numeric;
using Unit;

namespace IdleGame.Buff.BuffModels
{
    public class AddCriticalDamageBuffModel : BuffModel
    {
        public AddCriticalDamageBuffModel()
        {
            id       = "AddCriticalDamageBuffModel";
            name     = "增加暴击伤害";
            priority = 1;
            maxStack = 1000;
            tags     = null;
            tickTime = 0;
            stateMod = ChaControlState.Orgin;
        }

        public override void OnOccur(BuffObj buff, int modifyStack)
        {
            base.OnOccur(buff, modifyStack);
            buff.carrier.GetComponent<Entity>()
                .ModifyAttribute(AttributeType.CriticalDamage, modifyStack * 0.1f, ModifyNumericType.Add);
        }

        public override void OnRemoved(BuffObj buff)
        {
            base.OnRemoved(buff);
            buff.carrier.GetComponent<Entity>()
                .ModifyAttribute(AttributeType.CriticalDamage, -buff.stack * 0.1f, ModifyNumericType.Add);
        }
    }
}
#pragma warning restore 0649