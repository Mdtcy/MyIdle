/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月18日
 * @modify date 2022年10月18日
 * @desc [ItemTest
 */

#pragma warning disable 0649
using HM;
using HM.GameBase;
using NewLife.Config;

namespace Game.Test
{
    public class ItemTest : ItemBase<TestConfig>
    {
        [ES3Serializable]
        public int score;

        public ItemTest() : base()
        {
        }

        public ItemTest(int itemId) : base(itemId)
        {
        }

        public override void OnPicked()
        {
            base.OnPicked();
        }

        public override void OnLoaded()
        {
            base.OnLoaded();
        }
    }
}
#pragma warning restore 0649