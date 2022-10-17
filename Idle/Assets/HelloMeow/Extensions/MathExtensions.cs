/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-10-25 17:10:22
 * @modify date 2019-10-25 17:10:22
 * @desc [description]
 */

using UnityEngine;

namespace HM.Extensions
{
    public static class MathExtensions
    {
        public static float MinMax(float value, float min, float max)
        {
            Debug.Assert(min <= max);
            return Mathf.Min(max, Mathf.Max(min, value));
        }

        /// <summary>
        /// 让from向to方向插值，大小为amount
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float Approach(float from, float to, float amount)
        {
            if (from < to)
            {
                from += amount;

                if (from > to)
                {
                    return to;
                }
            }
            else
            {
                from -= amount;

                if (from < to)
                {
                    return to;
                }
            }

            return from;
        }

        /// <summary>
        /// 将在min和max之间的value映射到remapMin和remapMax之间 eg. 0-10之间的5，映射到0-100.就是50
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="remapMin"></param>
        /// <param name="remapMax"></param>
        /// <returns></returns>
        public static float Remap(float value, float min, float max, float remapMin, float remapMax)
        {
            float remappedValue = remapMin + (value - min) / (max - min) * (remapMax - remapMin);

            return remappedValue;
        }

        /// <summary>
        /// 四舍五入，numberOfDecimals是保留的位数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberOfDecimals"></param>
        /// <returns></returns>
        public static float RoundToDecimal(float value, int numberOfDecimals)
        {
            if (numberOfDecimals <= 0)
            {
                return Mathf.Round(value);
            }
            else
            {
                return Mathf.Round(value * 10f * numberOfDecimals) / (10f * numberOfDecimals);
            }
        }
    }
}