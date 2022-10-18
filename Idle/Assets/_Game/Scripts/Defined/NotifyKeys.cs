/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-01 10:06:53
 * @modify date 2020-06-01 10:06:53
 * @desc [description]
 */

using HM.Notification;

namespace NewLife.Defined
{
    public class NewLifeNotifyKeys : NotifyKeys
    {
        // ------- 从20001开始，切记不要重复 -------
        public static int EvtOnArchiveLoaded                   = 20011; // 存档加载成功
        public static int EvtOnNewChatMessageArrived           = 20012; // 收到新聊天信息
        public static int EvtOnSetMeowChatTitle                = 20013; // 设置喵信标题
        public static int EvtOnCluePickedToUnlock              = 20014; // ClueDirector随机挑选了待解锁的线索
        public static int EvtOnFollowAnonymously               = 20015; // 匿名关注成功
        public static int EvtOnConfirmToUnlockClueByHint       = 20016; // 使用提示解锁线索
        public static int EvtOnUnlockClue                      = 20017; // 解锁指定线索
        public static int EvtOnPlayerToggleLikes               = 20018; // 玩家点赞操作
        public static int EvtOnBeginChatWithPlayer             = 20019; // 当联系人和玩家开始新的对话时会发送该消息
        public static int EvtOnSwitchWeather                   = 20020; // 切换天气
        public static int EvtOnScheduleStateChanged            = 20021; // 行程状态变化
        public static int EvtOnDetectiveStateChanged           = 20022; // 侦探状态变化
        public static int EvtOnMemoryChecked           = 20023; // 首次查看回忆
        public static int EvtOnCurrencyNumChanged              = 20024; // 货币数量变化
        public static int EvtOnOpenTimeline                    = 20025; // 打开喵圈
        public static int EvtOnHasInvestigationResult          = 20026; // 显示侦查进度
        public static int EvtOnBeginInvestigationResultGuide   = 20027; // 显示侦查进度引导开始时
        public static int EvtOnOpenFindCluesInTimeline         = 20028; // 打开查找线索UI
        public static int EvtOnNeedToReply                     = 20029; // 需要回复联系人消息
        public static int EvtOnClueUnlockedPosition            = 20030; // 线索解锁的位置
        public static int EvtCluePackPostTipHidden             = 20031; // 找到全部线索的提示隐藏后
        public static int EvtOnItemMemoryPicked                = 20032; // 拾取新的回忆
        public static int EvtOnStartHighlightingClue           = 20033; // 开始播放高亮线索的动画
        public static int EvtOnContactPropertyChangedTip       = 20034; // 联系人属性变化TipUI提示
        public static int EvtOnBackgroundMessageArrived        = 20035; // 后台接受到消息
        public static int EvtOnScreenClicked                   = 20036; // 屏幕点击
        public static int EvtOnInvestigationRecordStateChanged = 20037; // 侦查记录状态改变
        public static int EvtOnUnlockedShopCommodity           = 20038; // 解锁奇迹小店商品
        public static int EvtOnSceneChatHidden                 = 20039; // 场景对话隐藏
        public static int EvtOnConfirmToArrived                = 20040; // 确认收货
        public static int EvtOnBeginNewbieSchedule             = 20041; // 开始新手行程
        public static int EvtOnNewbieScheduleFinished          = 20042; // 新手行程结束
    }
}