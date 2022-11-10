/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年11月6日
 * @modify date 2022年11月6日
 * @desc [货币操作接口]
 */

using System;

#pragma warning disable 0649
namespace Game.Currency
{
    public interface ICurrencyOperator
    {
        int CoinNum { get; }

        bool TryToConsumeCoin(int num);

        void AddCoin(int num);

        Action ActOnCoinNumChanged { get; set; }
    }
}
#pragma warning restore 0649