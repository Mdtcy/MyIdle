/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2021/09/22 15:02
 * @modify date 2021/09/22 15:02
 * @desc [插值辅助工具类]
 */

using UnityEngine;

#pragma warning disable 0649
namespace HM.Extensions
{
    /// <summary>
    /// 插值辅助工具类
    /// </summary>
    public static class LerpHelper
    {
        /// <summary>
        /// 返回一个与时间无关的阻尼，在阻尼为0时返回1
        /// </summary>
        /// <param name="damping"></param>
        /// <param name="elapsed"></param>
        /// <returns></returns>
        public static float GetDampenFactor(float damping, float elapsed)
        {
            if (damping < 0.0f)
            {
                return 1.0f;
            }

            return 1.0f - Mathf.Exp(-damping * elapsed);
        }
    }
}

#pragma warning restore 0649