/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月4日
 * @modify date 2023年1月4日
 * @desc []
 */

#pragma warning disable 0649
namespace Numeric
{
    public enum AttributeType
    {
        Atk = 1000,

        MaxHp = 2000,

        CriticalChance = 3000,

        CriticalDamage = 3100,

        // todo 还没实现
        DodgeChance = 4000,

        // todo 20-700
        AttackSpeed = 5000,

        BaseFireInterval = 6000,
    }

    public class AttributesNumeric : NumericBase<AttributeType>
    {
    }
}
#pragma warning restore 0649