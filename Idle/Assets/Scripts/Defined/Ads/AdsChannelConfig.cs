/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-25 21:12:31
 * @modify date 2022-08-25 21:12:31
 * @desc [单渠道广告配置]
 */

using System;
using System.Collections.Generic;
using HM.Extensions;
using Sirenix.OdinInspector;

#pragma warning disable 0649
namespace NewLife.Defined.Ads
{
    [Serializable]
    public class AdsChannelConfig
    {
        [LabelText("AppId")]
        public string AppId;

        [LabelText("AppName")]
        public string AppName;

        [LabelText("渠道")]
        public ChannelId channelId;

        [LabelText("广告位Id")]
        public List<AdsUnitConfig> UnitConfigs;

        /// <inheritdoc />
        public override string ToString()
        {

            return $"[AdsChannelConfig {AppId} / {channelId} / {UnitConfigs.ListToString()}]";
        }
    }
}
#pragma warning restore 0649