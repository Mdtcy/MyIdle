/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc [增加攻速]
 */

using Numeric;
using Unit;

#pragma warning disable 0649
namespace IdleGame.Buff.BuffModels
{
    public class AddAtkSpeedBuffModel : BuffModel
    {
        public AddAtkSpeedBuffModel()
        {
            id                = "AddAtkSpeedBuffModel";
            name              = "增加攻速";
            priority          = 1;
            maxStack          = 1000;
            tags              = null;
            tickTime          = 0;
            stateMod = ChaControlState.Orgin;
        }

        public override void OnOccur(BuffObj buff, int modifyStack)
        {
            base.OnOccur(buff, modifyStack);
            buff.carrier.GetComponent<Entity>()
                .ModifyAttribute(AttributeType.AttackSpeed, modifyStack, ModifyNumericType.Add);
        }

        public override void OnRemoved(BuffObj buff)
        {
            base.OnRemoved(buff);
            buff.carrier.GetComponent<Entity>()
                .ModifyAttribute(AttributeType.AttackSpeed, -buff.stack, ModifyNumericType.Add);
        }
    }
}
#pragma warning restore 0649