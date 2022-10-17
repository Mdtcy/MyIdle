/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-12 13:28:00
 * @modify date 2022-09-12 13:28:00
 * @desc []
 */

using System;
using HM.Date;
using UnityEngine;

#pragma warning disable 0649
namespace NewLife.BusinessLogic.DateTimeUtils
{
    public class DebugDateTime : IDateTime
    {
        private DateTime startDate;

        public DebugDateTime(string initDate)
        {
            startDate = System.DateTime.Parse(initDate);
        }

        /// <inheritdoc />
        public DateTime Now()
        {
            return startDate.AddSeconds(Time.time);
        }

        /// <inheritdoc />
        public DateTime Tomorrow()
        {
            return Today().AddDays(1);
        }

        /// <inheritdoc />
        public DateTime Today()
        {
            return Now().Date;
        }

        /// <inheritdoc />
        public DateTime DateTime(long ts)
        {
            return new DateTime(ts);
        }

        /// <inheritdoc />
        public bool IsHoliday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
#pragma warning restore 0649