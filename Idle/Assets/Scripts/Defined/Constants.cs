/**
 * @author [Boluo]
 * @email [tktetb@163.com]
 * @create date 2020-1-5
 * @modify date 2020-1-5
 * @desc [全局常量]
 */

using System.IO;
using UnityEngine;

#pragma warning disable 0649

/// <summary>
/// 全局常量
/// </summary>
public class Constants
{
    public static string RequestLocalCanSave = "RequestLocalCanSave";

    public const int ShopAppId            = 101001014; // 奇迹小铺app ID
    public const int TreasureAppId        = 101001015; // 奇迹珍藏app ID
    public const int InteractionRoomAppId = 101001016; // 虚拟空间App ID
    public const int WeatherAppId         = 101001011; // 天气app ID
    public const int SettingAppId         = 101001009; // 设置app ID
    public const int MailAppId            = 101001017; // 邮件app ID

    public const int GCoinId       = 108001002; // G元Id
    public const int HpId          = 108002001; // 小电池Id
    public const int ContactHaluId = 102001008; // 哈鲁联系人Id

    public const string TotalScheduleCountKey  = "TotalScheduleCount";
    public const string GCoinCountKey          = "GCoinCount";
    public const string HpCountKey             = "HpCount";
    public const string UnlockScheduleCountKey = "UnlockScheduleCount";
    public const string ModTsKey               = "ModTs";

    public const int AdsStartId = 911001000; // 广告起始Idx

    public static string RemoteConfigLocalPath()
    {
        return Path.Combine(Application.persistentDataPath, "RemoteConfig.json");
    }
}

#pragma warning restore 0649