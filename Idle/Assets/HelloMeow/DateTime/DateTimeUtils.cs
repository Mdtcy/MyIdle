/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-01-01 18:37:30
 * @modify date 2019-01-01 18:37:30
 * @desc [封装日期时间的接口]
 */
using System;

namespace HM.DateTimeUtils
{
    /// <summary>
    /// 封装了获得日期时间的函数接口
    /// </summary>
    public static class DateTimeUtils
    {
        #region FIELDS
        private static readonly string[] Weekdays = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// 返回现在的日期对象
        /// </summary>
        /// <returns></returns>
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 从0:00:00 UTC on January 1, 0001到现在的tick数
        /// </summary>
        /// <returns></returns>
        public static long Ticks()
        {
            return Now().Ticks;
        }

        /// <summary>
        /// 从0:00:00 UTC on January 1, 0001到现在的毫秒数
        /// </summary>
        /// <returns></returns>
        public static long Milliseconds()
        {
            return Now().Ticks / 10000;
        }

        /// <summary>
        /// 从0:00:00 UTC on January 1, 0001到现在的秒数
        /// </summary>
        /// <returns></returns>
        public static long Seconds()
        {
            return Now().Ticks / 10000000;
        }

        public static long Seconds2Ticks(long seconds)
        {
            return seconds * 10000000;
        }

        public static string DayOfWeek(DateTime date)
        {
            return Weekdays[Convert.ToInt32(date.DayOfWeek.ToString("d"))];
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