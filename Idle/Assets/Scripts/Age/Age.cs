/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月25日
 * @modify date 2022年10月25日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using GameFramework;
using HM.Interface;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Age
{
    public class Age : PersistableObject, ITickable
    {
        #region FIELDS

        [ES3Serializable]
        public int year;

        [ES3Serializable]
        public int day;

        public Action ActOnAgeChange;

        // 每年天数
        private const int YearDayCount = 365;

        #endregion

        #region PROPERTIES

        public int Day => day;

        public int Year => year;

        public override string ToString()
        {
            return $"{Year}年{Day}天";
        }

        #endregion

        #region PUBLIC METHODS

        public override void Initialize()
        {

        }

        private float dayTime = 0.2f;
        private float dayTimer;
        public void Tick()
        {
            if (dayTimer >= 0)
            {
                dayTimer -= Time.deltaTime;
            }
            else
            {
                dayTimer = dayTime;
                AddDay(1);
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        public void AddDay(int value)
        {
            if (day + value > YearDayCount)
            {
                int totalDay = day + value;

                // 加完刚好在一年最后一天
                if (totalDay % YearDayCount == 0)
                {
                    day  =  YearDayCount;
                    year += totalDay / YearDayCount - 1;
                }
                else
                {
                    day  =  totalDay % YearDayCount;
                    year += totalDay / YearDayCount;
                }
            }
            else
            {
                day += value;
            }

            ActOnAgeChange?.Invoke();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649