/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-25 21:12:51
 * @modify date 2022-08-25 21:12:51
 * @desc [广告位配置]
 */

using System;
using Sirenix.OdinInspector;

#pragma warning disable 0649
namespace NewLife.Defined.Ads
{
    [Serializable]
    public class AdsUnitConfig
    {
        [LabelText("广告位类型")]
        public AdsUnitType UnitType;

        [LabelText("广告位Id")]
        public string UnitId;

        [LabelText("广告间隔CD(秒)")]
        public int Cd;

        /// <summary>
        /// Id
        /// </summary>
        public int Id => Constants.AdsStartId + (int) UnitType;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{UnitType}:{UnitId}:{Cd}]";
        }
    }
}
#pragma warning restore 0649