/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc [增加一百攻速]
 */

using Numeric;

#pragma warning disable 0649
namespace IdleGame.Buff.BuffModels
{
    public class Add100AtkSpeedBuffModel : BuffModel
    {
        public Add100AtkSpeedBuffModel()
        {
            id                = "Add100AtkSpeedBuffModel";
            name              = "增加一百攻速";
            priority          = 1;
            maxStack          = 10;
            tags              = null;
            tickTime          = 0;
        }

        public override void OnOccur(BuffObj buff, int modifyStack)
        {
            base.OnOccur(buff, modifyStack);
            buff.carrier.GetComponent<Entity>().ModifyAttribute(AttributeType.AttackSpeed, 100 * modifyStack, ModifyNumericType.Add);
        }

        public override void OnRemoved(BuffObj buff)
        {
            base.OnRemoved(buff);
            buff.carrier.GetComponent<Entity>()
                .ModifyAttribute(AttributeType.AttackSpeed, -100 * buff.stack, ModifyNumericType.Add);
        }
    }
}
#pragma warning restore 0649