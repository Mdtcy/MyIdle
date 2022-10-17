/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-07-05 11:20:48
 * @modify date 2019-07-05 11:20:48
 * @desc [description]
 */

using System.Collections.Generic;
using HM.Defined;

namespace HM.ConvertHelper
{
    public static class ConvertHelper
    {
        #region Great Chinese
        private static readonly List<string> ChineseGreatNumberList0 = new List<string>()
        {
            "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖"
        };

        private static readonly List<string> ChineseGreatNumberList1 = new List<string>()
        {
            "拾", "佰", "仟", "万"
        };
        /// <summary>
        /// 将阿拉伯数字转为中文大写，比如123->壹佰贰拾叁
        /// TODO: 目前只支持个位数，从0-9
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string NumberToGreatChinese(int num)
        {
            if (num < 0)
            {
                return "不支持负数";
            }

            // num > 10
            if (num >= 100)
            {
                return "不支持>=100的数";
            }

            if (num < 10)
            {
                return ChineseGreatNumberList0[num];
            }

            if (num == 10)
            {
                return ChineseGreatNumberList1[0];
            }

            // 11 - 19 不显示壹
            if (num < 20)
            {
                return $"{ChineseGreatNumberList1[0]}{ChineseGreatNumberList0[num % 10]}";
            }

            int h = num / 10;
            int l = num % 10;

            if (l == 0)
            {
                // 整数只显示两位，比如三十，不能显示三十零
                return $"{ChineseGreatNumberList0[h]}{ChineseGreatNumberList1[0]}";
            }
            else
            {
                return $"{ChineseGreatNumberList0[h]}{ChineseGreatNumberList1[0]}{ChineseGreatNumberList0[l]}";
            }
        }
        #endregion

        #region Great Chinese
        private static readonly List<string> ChineseNumberList0 = new List<string>()
        {
            "零", "一", "二", "三", "四", "五", "六", "七", "八", "九"
        };

        private static readonly List<string> ChineseNumberList1 = new List<string>()
        {
            "十", "百", "千", "万"
        };
        /// <summary>
        /// 将阿拉伯数字转为中文大写，比如123->壹佰贰拾叁
        /// TODO: 目前只支持个位数，从0-9
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string NumberToChinese(int num)
        {
            if (num < 0)
            {
                return "不支持负数";
            }

            // num > 10
            if (num >= 100)
            {
                return "不支持>=100的数";
            }

            if (num < 10)
            {
                return ChineseNumberList0[num];
            }

            if (num == 10)
            {
                return ChineseNumberList1[0];
            }

            // 11 - 19 不显示壹
            if (num < 20)
            {
                return $"{ChineseNumberList1[0]}{ChineseNumberList0[num % 10]}";
            }

            int h = num / 10;
            int l = num % 10;

            if (l == 0)
            {
                // 整数只显示两位，比如三十，不能显示三十零
                return $"{ChineseNumberList0[h]}{ChineseNumberList1[0]}";
            }
            else
            {
                return $"{ChineseNumberList0[h]}{ChineseNumberList1[0]}{ChineseNumberList0[l]}";
            }
        }
        #endregion

        #region Date
        private static readonly List<string> ChineseMonthList = new List<string>
        {
            "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"
        };
        /// <summary>
        /// 数字月份转为中文月份
        /// </summary>
        /// <param name="monthIndex">1~12</param>
        /// <returns></returns>
        public static string ToChineseMonth(int monthIndex)
        {
            HMLog.Assert(monthIndex >= 1 && monthIndex <= 12, $"Month({monthIndex} must between 1~12");

            return ChineseMonthList[monthIndex - 1];
        }

        private static readonly List<string> ChineseNums0 = new List<string> { "", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        private static readonly List<string> ChineseNums1 = new List<string> { "", "十", "廿", "卅" };
        /// <summary>
        /// 数字日子转为农历字符串
        /// </summary>
        /// <param name="dayIndex">1~30</param>
        /// <returns></returns>
        public static string ToLunarDay(int dayIndex)
        {
            HMLog.Assert(dayIndex >= 1 && dayIndex <= 30, $"Day{dayIndex} must between 1~30");
            int a = dayIndex / 10;
            int r = dayIndex % 10;
            return (a > 0 ? ChineseNums1[a] : "") + ChineseNums0[r];
        }

        /// <summary>
        /// 数字关系的名字
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static string NumericRelationName(NumericRelation relation)
        {
            switch (relation)
            {
                case NumericRelation.GreaterThan:
                    return "大于";
                case NumericRelation.GreaterThanOrEqual:
                    return "大于等于";
                case NumericRelation.Equal:
                    return "等于";
                case NumericRelation.LessThan:
                    return "小于";
                case NumericRelation.LessThanOrEqual:
                    return "小于等于";
                default:
                    HMLog.LogWarning("[IsNumericRelationMet] unsupported operator! {0}", relation);
                    return "无效";
            }
        }

        #endregion
    }
}