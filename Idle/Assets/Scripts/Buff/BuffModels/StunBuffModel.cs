/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc []
 */

#pragma warning disable 0649
using Unit;

namespace IdleGame.Buff.BuffModels
{
    public class StunBuffModel : BuffModel
    {
        public StunBuffModel()
        {
            id       = "StunBuffModel";
            name     = "眩晕";
            priority = 1;
            maxStack = 10;
            tags     = null;
            tickTime = 0;
            stateMod = ChaControlState.Stun;
        }
    }
}
#pragma warning restore 0649