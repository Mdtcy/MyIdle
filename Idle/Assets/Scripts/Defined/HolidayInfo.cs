/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-05 18:10:36
 * @modify date 2022-09-05 18:10:36
 * @desc [description]
 */

using LitJson;

#pragma warning disable 0649
namespace NewLife.Defined
{
    public struct HolidayInfo
    {
        public int year;
        public int month;
        public int date;
        public int yearweek;
        public int yearday;
        public int lunar_year;
        public int lunar_month;
        public int lunar_date;
        public int lunar_yearday;
        public int week;
        public int weekend;
        public int workday;
        public int holiday;
        public int holiday_or;
        public int holiday_overtime;
        public int holiday_today;
        public int holiday_legal;
        public int holiday_recess;

        public HolidayInfo(JsonData data)
        {
            year             = (int) data["year"];
            year             = (int) data["year"];
            month            = (int) data["month"];
            date             = (int) data["date"];
            yearweek         = (int) data["yearweek"];
            yearday          = (int) data["yearday"];
            lunar_year       = (int) data["lunar_year"];
            lunar_month      = (int) data["lunar_month"];
            lunar_date       = (int) data["lunar_date"];
            lunar_yearday    = (int) data["lunar_yearday"];
            week             = (int) data["week"];
            weekend          = (int) data["weekend"];
            workday          = (int) data["workday"];
            holiday          = (int) data["holiday"];
            holiday_or       = (int) data["holiday_or"];
            holiday_overtime = (int) data["holiday_overtime"];
            holiday_today    = (int) data["holiday_today"];
            holiday_legal    = (int) data["holiday_legal"];
            holiday_recess   = (int) data["holiday_recess"];
        }

        public bool IsHoliday()
        {
            return holiday != 10;
        }
    }
}
#pragma warning restore 0649