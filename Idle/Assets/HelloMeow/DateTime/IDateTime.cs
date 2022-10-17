/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-04 18:07:09
 * @modify date 2020-07-04 18:07:09
 * @desc [description]
 */

using System;

namespace HM.Date
{
    public interface IDateTime
    {
        /// <summary>
        /// 获取今日日期
        /// </summary>
        /// <returns></returns>
        System.DateTime Now();

        /// <summary>
        /// 获取明天的日期(0点0分0秒)
        /// </summary>
        /// <returns></returns>
        System.DateTime Tomorrow();

        /// <summary>
        /// 当天0点0分0秒的时刻
        /// </summary>
        /// <returns></returns>
        System.DateTime Today();

        /// <summary>
        /// 根据ts创建DateTime对象
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        System.DateTime DateTime(long ts);

        /// <summary>
        /// 是否节假日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        bool IsHoliday(DateTime date);
    }
}
