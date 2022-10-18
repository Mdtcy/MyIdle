/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月18日
 * @modify date 2022年10月18日
 * @desc [ItemTest
 */

#pragma warning disable 0649
using HM.GameBase;
using NewLife.Config;

namespace DefaultNamespace.Test
{
    public class ItemTest : ItemBase<TestConfig>
    {
        public ItemTest() : base()
        {
        }

        public ItemTest(int itemId) : base(itemId)
        {
        }
    }
}
#pragma warning restore 0649