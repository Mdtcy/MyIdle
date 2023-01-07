/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月6日
 * @modify date 2023年1月6日
 * @desc []
 */

#pragma warning disable 0649
using IdleGame;
using Numeric;

namespace Unit
{
    public class AddPoint1CriticalProb : Buff
    {
        private Entity owener;

        private float criticalProbChanged;

        public AddPoint1CriticalProb(Entity entity)
        {
            owener = entity;
        }

        public override void OnOccur(int modStack)
        {
            criticalProbChanged = 0.1f * modStack;
            owener.ModifyAttribute(AttributeType.CriticalProbability, 0.1f, ModifyNumericType.Add);
        }

        public override string Id()
        {
            return "AddCriticalProb";
        }
    }
}
#pragma warning restore 0649