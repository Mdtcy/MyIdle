/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-04 17:53:40
 * @modify date 2022-09-04 17:53:40
 * @desc []
 */

using System;

#pragma warning disable 0649
namespace NewLife.BusinessLogic.DateTimeUtils
{
    public static class DateTimeExtensions
    {
        public static DateTime UnixTimeToDateTime(long unixTime)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return dt.AddMilliseconds(unixTime).ToLocalTime();
        }

        public static long ToUnixTime(this DateTime self)
        {
            var timeSpan = self - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)timeSpan.TotalSeconds;
        }
    }
}
#pragma warning restore 0649