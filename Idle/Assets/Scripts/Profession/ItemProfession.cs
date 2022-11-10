/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月29日
 * @modify date 2022年10月29日
 * @desc [职业]
 */

#pragma warning disable 0649
using System;
using Game.Currency;
using Game.PlayerAge;
using HM;
using HM.GameBase;
using NewLife.Config;
using Zenject;

namespace Profession
{
    /// <summary>
    /// 职业
    /// </summary>
    public class ItemProfession : ItemBase<ProfessionConfig>
    {
        #region FIELDS

        // inject
        [Inject]
        private Age age;

        [Inject]
        private ICurrencyOperator currencyOperator;

        [ES3Serializable]
        private int level;

        // 经验 升级时清零
        [ES3Serializable]
        private int exp;

        public Action ActOnExpChanged;

        #endregion

        #region PROPERTIES

        public int Exp
        {
            get => exp;
            set
            {
                if (value == exp)
                {
                    return;
                }

                HMLog.Assert(value < Config.ExpToNextLevel(level));
                exp = value;
                ActOnExpChanged?.Invoke();
            }
        }

        public int Level
        {
            get => level;
        }

        #endregion

        #region PUBLIC METHODS

        public ItemProfession() : base()
        {
        }

        public ItemProfession(int itemId) : base(itemId)
        {
        }

        public override void OnPicked()
        {
            base.OnPicked();
            age.ActOnAgeDayChange += OnAgeDayChange;
        }

        public override void OnLoaded()
        {
            base.OnLoaded();
            age.ActOnAgeDayChange += OnAgeDayChange;
        }

        /// <summary>
        /// 到下一级还需的经验值
        /// </summary>
        /// <returns></returns>
        public int RemainExpToLevelUp()
        {
            return Config.ExpToNextLevel(level) - Exp;
        }

        /// <summary>
        /// 这一级升级所需的总经验值
        /// </summary>
        /// <returns></returns>
        public int TotalExpToNextLevel()
        {
            return Config.ExpToNextLevel(level);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnAgeDayChange()
        {
            // 获得薪水
            int salary = Config.SalaryPerDay(level);
            currencyOperator.AddCoin(salary);

            // 获得经验 (可能不止只能升一级)
            int getExp = ExpPerDay();

            while (getExp > 0)
            {
                // 经验够升下一级，则进行升级
                if (getExp >= RemainExpToLevelUp())
                {
                    getExp -= RemainExpToLevelUp();
                    level  += 1;
                    Exp    =  0;
                }
                else
                {
                    Exp    += getExp;
                    getExp =  0;
                }
            }
        }

        private int ExpPerDay()
        {
            return 1;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649