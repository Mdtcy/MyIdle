/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-04 19:07:20
 * @modify date 2020-07-04 19:07:20
 * @desc [本机日期时间（未与服务器校正）]
 */

using System;
using HM.Date;

namespace NewLife.BusinessLogic.DateTimeUtils
{
    public class SimpleDateTime : IDateTime
    {
        /// <inheritdoc />
        public System.DateTime Now()
        {
            return System.DateTime.Now;
        }

        /// <inheritdoc />
        public System.DateTime Tomorrow()
        {
            var now = Now().AddDays(1);
            return new System.DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        }

        /// <inheritdoc />
        public System.DateTime Today()
        {
            return System.DateTime.Today;
        }

        /// <inheritdoc />
        public System.DateTime DateTime(long ts)
        {
            return new System.DateTime(ts);
        }

        /// <inheritdoc />
        public bool IsHoliday(DateTime date)
        {
            // 本地只能区分周六和周日
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
