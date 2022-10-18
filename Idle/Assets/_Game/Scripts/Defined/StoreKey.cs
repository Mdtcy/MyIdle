/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-13 16:06:14
 * @modify date 2020-06-13 16:06:14
 * @desc [用在KeyValue*内的key定义]
 */

using System;

namespace NewLife.Defined
{
    /// <summary>
    /// 除了ItemId可以直接做kv的key以外，其他情况需在此定义。
    /// </summary>
    [Serializable]
    public class StoreKey
    {
        // placeholder
        public const int IsScheduleNotificationEnabled      = 902000005; // 行程通知是否打开了
        public const int IsStoryScheduleNotificationEnabled = 902000006; // 故事行程通知是否打开了
        public const int NewbieGiftPackClaimed              = 902000007; // 是否领取了新手礼包
        public const int ExchangeHpByFixedNum               = 902000008; // 指定行程缺小电池时只能固定兑换小电池
        public const int NewbieGiftPackActivated            = 902000009; // 只有1020版本之后的新玩家才会触发新手礼包

        // ---------- KvStrBool ----------
        public const string RequestLocalWithOptionalSaveEnabled = "RequestLocalWithOptionalSaveEnabled"; // RequestLocalWithOptionalSave是否可以保存
    }
}