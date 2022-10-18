/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-10-05 10:07:28
 * @modify date 2022-10-05 10:07:28
 * @desc [UiDialogType]
 */

using System;

#pragma warning disable 0649
namespace NewLife.Defined
{
    [Serializable]
    public struct UiDialogType
    {
        /// <summary>
        /// 类型名
        /// </summary>
        public readonly string Name;

        public UiDialogType(string name)
        {
            Name = name ?? string.Empty;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"UiDialogTypeDefined.{Name}";
        }

        public static bool operator ==(UiDialogType a, UiDialogType b)
        {
            return a.Name.Equals(b.Name, StringComparison.Ordinal);
        }

        public static bool operator !=(UiDialogType a, UiDialogType b)
        {
            return !a.Name.Equals(b.Name, StringComparison.Ordinal);
        }

        public bool Equals(UiDialogType other)
        {
            return Name == other.Name;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is UiDialogType other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public static implicit operator UiDialogType(string someValue)
        {
            return string.IsNullOrEmpty(someValue) ? NotSpecified : new UiDialogType(someValue);
        }

        public static readonly UiDialogType NotSpecified             = new UiDialogType("NotSpecified");             // 未指定
        public static readonly UiDialogType UIContactIntro           = new UiDialogType("UIContactIntro");           // 联系人简介ui
        public static readonly UiDialogType UIShowFileImage          = new UiDialogType("UIShowFileImage");          // 显示图片介绍
        public static readonly UiDialogType UIFileItemIntro          = new UiDialogType("UIFileItemIntro");          // 显示物品介绍
        public static readonly UiDialogType UIUserAgreement          = new UiDialogType("UIUserAgreement");          // 用户协议
        public static readonly UiDialogType UIUserAgreementIceSimba  = new UiDialogType("UIUserAgreementIceSimba");  // 冰狮用户协议
        public static readonly UiDialogType UILoginByPhone           = new UiDialogType("UILoginByPhone");           // 手机登录
        public static readonly UiDialogType UIRegisterByPhone        = new UiDialogType("UIRegisterByPhone");        // 手机注册
        public static readonly UiDialogType UIResetPassword          = new UiDialogType("UIResetPassword");          // 重置密码
        public static readonly UiDialogType UIUnderageTip            = new UiDialogType("UIUnderageTip");            // 未成年提示
        public static readonly UiDialogType UIRealNameAuth           = new UiDialogType("UIRealNameAuth");           // 实名认证
        public static readonly UiDialogType UILoadingIndicator       = new UiDialogType("UILoadingIndicator");       // 模拟转圈效果
        public static readonly UiDialogType UIReviewDialogueTree     = new UiDialogType("UIReviewDialogueTree");     // 回顾对话
        public static readonly UiDialogType UIQuitGameTip            = new UiDialogType("UIQuitGameTip");            // 退出游戏提示
        public static readonly UiDialogType UIMailDetail             = new UiDialogType("UIMailDetail");             // 邮件详情
        public static readonly UiDialogType UIMailDetailHasRewards   = new UiDialogType("UIMailDetailHasRewards");   // 邮件详情（有奖励）
        public static readonly UiDialogType UINotice                 = new UiDialogType("UINotice");                 // 公告UI
        public static readonly UiDialogType UIAllStaff               = new UiDialogType("UIAllStaff");               // 全员
        public static readonly UiDialogType UIStoryList              = new UiDialogType("UIStoryList");              // 特殊故事列表
        public static readonly UiDialogType UIEditDressUp            = new UiDialogType("UIEditDressUp");            // 编辑装扮
        public static readonly UiDialogType UIRecordedUtteranceList  = new UiDialogType("UIRecordedUtteranceList");  // 语录列表
        public static readonly UiDialogType UISendFlower             = new UiDialogType("UISendFlower");             // 送花
        public static readonly UiDialogType UIVent                   = new UiDialogType("UIVent");                   // 发泄
        public static readonly UiDialogType UIScheduleSceneChat      = new UiDialogType("UIScheduleSceneChat");      // 行程场景对话
        public static readonly UiDialogType UINotRecordSceneChat     = new UiDialogType("UINotRecordSceneChat");     // 不记录完成状态场景对话
        public static readonly UiDialogType UIUploadArchive          = new UiDialogType("UIUploadArchive");          // 上传云存档
        public static readonly UiDialogType UIExchangeHpToPeep       = new UiDialogType("UIExchangeHpToPeep");       // 转换行动力为偷窥点
        public static readonly UiDialogType UICurrencyShortage       = new UiDialogType("UICurrencyShortage");       // 货币不足
        public static readonly UiDialogType UIRealNameAuth3rdParty   = new UiDialogType("UIRealNameAuth3rdParty");   // 第三方实名认证
        public static readonly UiDialogType UIUserBlockedTip         = new UiDialogType("UIUserBlockedTip");         // 封号提示
        public static readonly UiDialogType UIHpSource               = new UiDialogType("UIHpSource");               // 小电池来源UI
        public static readonly UiDialogType UIGCoinSource            = new UiDialogType("UIGCoinSource");            // G元来源UI
        public static readonly UiDialogType UIUpdateApp              = new UiDialogType("UIUpdateApp");              // 更新App提示
        public static readonly UiDialogType UIContactUs              = new UiDialogType("UIContactUs");              // 联系我们
        public static readonly UiDialogType UIExchangeRewards        = new UiDialogType("UIExchangeRewards");        // 兑换奖励
        public static readonly UiDialogType UIJailBrokenWarning      = new UiDialogType("UIJailBrokenWarning");      // 破解提示
        public static readonly UiDialogType UIConfirmToUploadArchive = new UiDialogType("UIConfirmToUploadArchive"); // 确认是否上传云存档
        public static readonly UiDialogType UINewbieGiftPack         = new UiDialogType("UINewbieGiftPack");         // 新手玩家大礼包
        public static readonly UiDialogType UIExchangeHpByGCoin      = new UiDialogType("UIExchangeHpByGCoin");      // G元兑换小电池
        public static readonly UiDialogType UIFirstRecharge1Yuan     = new UiDialogType("UIFirstRecharge1Yuan");     // 1元首冲
        public static readonly UiDialogType UIWAMGameMenu            = new UiDialogType("UIWAMGameMenu");            // 打地鼠游戏菜单
        public static readonly UiDialogType UIWAMGamePause           = new UiDialogType("UIWAMGamePause");           // 打地鼠游戏暂停
        public static readonly UiDialogType UIWAMGameOver            = new UiDialogType("UIWAMGameOver");            // 打地鼠游戏结束
        public static readonly UiDialogType UIWAMAddTime             = new UiDialogType("UIWAMAddTime");             // 打地鼠游戏加时
        public static readonly UiDialogType DoubleReward812          = new UiDialogType("DoubleReward812");          // 812玩家双倍返利ui
    }
}
#pragma warning restore 0649