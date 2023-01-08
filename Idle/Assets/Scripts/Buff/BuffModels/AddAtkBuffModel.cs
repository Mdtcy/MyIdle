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
    public class AddAtkBuffModel : BuffModel
    {
        public AddAtkBuffModel()
        {
            id       = "AddAtkBuffModel";
            name     = "增加攻击力";
            priority = 1;
            maxStack = 999999;
            tags     = null;
            tickTime = 0;
            stateMod = ChaControlState.Orgin;
        }

        public override void OnOccur(BuffObj buff, int modifyStack)
        {
            base.OnOccur(buff, modifyStack);
            buff.carrier.GetComponent<Entity>()
                .ModifyAttribute(AttributeType.Atk, modifyStack, ModifyNumericType.Add);
        }

        public override void OnRemoved(BuffObj buff)
        {
            base.OnRemoved(buff);
            buff.carrier.GetComponent<Entity>()
                .ModifyAttribute(AttributeType.Atk, -buff.stack, ModifyNumericType.Add);
        }
    }
}
#pragma warning restore 0649