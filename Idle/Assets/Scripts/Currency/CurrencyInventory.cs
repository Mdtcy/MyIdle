/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年11月6日
 * @modify date 2022年11月6日
 * @desc [货币仓库 因为货币数量只会有几种 所以直接硬编码]
 */

#pragma warning disable 0649
using System;
using HM.Interface;

namespace Game.Currency
{
    public class CurrencyInventory : PersistableObject, ICurrencyOperator
    {
        #region FIELDS

        [ES3Serializable]
        private int coinNum;

        private Action actOnCoinNumChanged;

        #endregion

        #region PROPERTIES

        public Action ActOnCoinNumChanged
        {
            get => actOnCoinNumChanged;
            set => actOnCoinNumChanged = value;
        }

        public int CoinNum
        {
            get => coinNum;
            private set
            {
                if (value == coinNum)
                {
                    return;
                }

                coinNum = value;
                ActOnCoinNumChanged?.Invoke();
            }
        }

        #endregion

        #region PUBLIC METHODS

        public bool TryToConsumeCoin(int num)
        {
            if (num > CoinNum)
            {
                return false;
            }

            CoinNum -= num;

            return true;
        }

        public void AddCoin(int num)
        {
            CoinNum += num;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649