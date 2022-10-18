/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-25 23:05:07
 * @modify date 2020-05-25 23:05:07
 * @desc [description]
 */

using System;

namespace NewLife.Defined
{
    /// <summary>
    /// App icon尺寸类型
    /// </summary>
    public enum IconSizeType
    {
        Size1X1, // 占据1x1格子
        Size2X2, // 占据2x2格子
        Size3X1, // 占据3x1格子
        Size4X1, // 占据4x1格子
    }

    /// <summary>
    /// 喵信App主题样式
    /// </summary>
    public enum MeowChatTheme
    {
        Normal,    // 默认主题
        Anonymous, // 匿名主题
    }

    /// <summary>
    /// 提示ui的类型
    /// </summary>
    public enum TipUiType
    {
        Normal,                    // 正常操作流程触发的提示
        Warning,                   // 操作异常触发的提示
        Clue,                      // 线索相关的提示
        ScheduleUnlock,            // 行程解锁提示
        ScheduleFinish,            // 行程完成提示
        ContactPropertyChanged,    // 联系人属性变化提示
        CoinIncreased,             // 金币增加提示
        CoinDecreased,             // 金币减少提示
        PlayerConfidenceChanged,   // 主角自信变化提示
        PlayerToleranceChanged,    // 主角宽容变化提示
        OrderArrived,              // 订单已送达
        Shop,                      // 商店相关的TipUI
        HpIncreased,               // 体力增加
        HpDecreased,               // 体力减少
        InfiniteHp,                // 无限小电池
        InteractionPointIncreased, // 互动点增加
        SpecialScheduleUnlock,     // 特殊行程解锁提示
        JumpToUploadArchive,       // 跳转到上传云存档提示
        PeepIncreased,             // 偷窥点增加提示
        PeepDecreased,             // 偷窥点减少提示
    }

    /// <summary>
    /// 行程命令类型
    /// </summary>
    public enum ScheduleCommandType
    {
        Unlock, // 解锁行程
        Finish, // 完成行程
    }

    /// <summary>
    /// 行程状态
    /// </summary>
    public enum ScheduleState
    {
        Invalid,  // 未初始化状态
        Ongoing,  // 进行中
        Finished, // 已结束
    }

    /// <summary>
    /// 行程跳转的链接
    /// </summary>
    public enum JumpTarget
    {
        Chat,            // 聊天
        Timeline,        // 动态
        Contact,         // 联系人
        AboutMe,         // 我
        Schedule,        // 行程
        Investigation,   // 万事通
        MemoirZhaozeyu,  // 回忆录-赵泽宇
        MemoirSidaili,   // 回忆录-斯黛丽
        MemoirMeiqiu,    // 回忆录-煤球
        MemoirZhaozehui, // 回忆录-赵泽慧
        DialogueTree,    // 场景对话
        Shop,            // 奇迹小铺
        SpecialChatRoom, // 特殊故事喵信
    }

    /// <summary>
    /// 行程通知状态
    /// </summary>
    public enum ScheduleNotificationState
    {
        On,  // 通知打开
        Off, // 通知关闭
    }

    /// <summary>
    /// 特殊行程通知状态
    /// </summary>
    public enum StoryScheduleNotificationState
    {
        On,  // 通知打开
        Off, // 通知关闭
    }

    /// <summary>
    /// 侦探状态
    /// </summary>
    public enum DetectiveState
    {
        Idle,          // 空闲
        Investigating, // 侦查中
    }

    /// <summary>
    /// 侦查记录状态
    /// </summary>
    public enum InvestigationRecordState
    {
        Investigating, // 侦查中
        Committed,     // 已提交（侦查结果已提交，但玩家还未查看）
        Finished,      // 已完成（玩家已看过侦查结果）
    }

    // 切换背景的效果
    public enum ChangeBackgroundEffect
    {
        Fade                = 0, // 渐隐渐现（默认）
        DirectShow          = 1, // 直接显示
        Shake               = 2, // 画面震动
        OpenEyeOnce         = 3, // 睁眼一次即清晰
        OpenEyeMultipleBlur = 4, // 睁眼多次，从模糊到清晰
        ShakeOnce           = 5, // 震动一次，多表达惊讶的情绪
    }

    // 喵信对话效果
    public enum SayEffect
    {
        Nothing   = 0, // 什么也不做
        Shake     = 1, // 播放震动效果
        ShakeOnce = 2, // 播放震动一次的效果
    }

    /// <summary>
    /// 联系人不能出现的场景（例如：某联系人A不能出现在联系人列表）
    /// </summary>
    [Flags]
    public enum ContactBlockedScenario
    {
        ContactList      = 1 << 0, // 不能出现在联系人列表
        AppointDetective = 1 << 1, // 不能出现在委托侦查页面
    }

    /// <summary>
    /// 回忆状态
    /// </summary>
    public enum MemoryState
    {
        Unchecked, // 还没有查看过回忆对话
        Checked,   // 至少查看了一次回忆对话
    }

    /// <summary>
    /// 联系人的属性
    /// </summary>
    public enum ContactPropertyType
    {
        FavorToLME       = 1,  // 对林敏恩的好感度
        Confidence       = 2,  // 自信
        Tolerance        = 3,  // 宽容
        InteractionPoint = 4,  // 互动点
        FavorToZZY       = 5,  // 对赵泽宇好感度
        FavorToXX        = 6,  // 对小小好感度
        FavorToZZH       = 7,  // 对赵泽慧好感度
        FavorToSDL       = 8,  // 对斯黛丽好感度
        FavorToAL        = 9,  // 对艾拉好感度
        FavorToXQ        = 10, // 对小祁好感度
    }

    /// <summary>
    /// 条件类型
    /// </summary>
    public enum ConditionType : short
    {
        Unspecified   = 0, // 尚未指定
        ScheduleState = 1, // 行程状态条件
    }

    /// <summary>
    /// 命令类型
    /// </summary>
    public enum CommandType : short
    {
        Unspecified       = 0, // 未指定
        AcquireItem       = 1, // 获得(解锁)物品
        UpdateNpcProperty = 2, // 更新联系人属性
        SetScheduleState  = 3, // 设置行程状态
        SwitchWeather     = 4, // 切换天气
        CustomScript      = 5, // 自定义脚本命令
    }

    /// <summary>
    /// 可侦查线索状态
    /// </summary>
    public enum LeadType : byte
    {
        Unlocked     = 1, // 已解锁（默认，未侦查过）
        Investigated = 2, // 已经侦查过
    }

    /// <summary>
    /// 飘字UI的样式
    /// </summary>
    public enum TipUiTheme
    {
        Normal,
        SceneChat,
    }

    /// <summary>
    /// 控制音频播放的模式
    /// </summary>
    public enum AudioMode
    {
        Effect,  // 普通音效，只播放一次，无法手动停止
        Ambient, // 环境音，需要配合Stop节点停止播放
    }

    /// <summary>
    /// 关系运算子
    /// </summary>
    public enum RelationalOperator
    {
        Greater              = 0,
        GreaterThanOrEqualTo = 1,
        Less                 = 2,
        LessThanOrEqualTo    = 3,
        Equal                = 4,
        NotEqual             = 5
    }

    /// <summary>
    /// 天气类型
    /// </summary>
    public enum WeatherType
    {
        Sunny     = 0, // 晴天
        Overcast  = 1, // 阴天
        LightRain = 2, // 小雨
        HeavyRain = 3, // 大雨
        Snow      = 4  // 雪天
    }

    /// <summary>
    /// 平台类型
    /// </summary>
    public enum PlatformType : short
    {
        IOS     = 1,
        Android = 2,
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum PurchaseOrderState : short
    {
        Created      = 0, // 刚创建，尚未支付
        PossiblyPaid = 1, // 可能已支付（需要查询确认）
        Delivered    = 2, // 已发货
        Finished     = 3, // 已完成：服务端也标记为已发货
        Paid         = 4, // 确认已支付（查询未发货订单时或订单校验成功后设置）
    }

    /// <summary>
    /// 订单支付结果状态
    /// </summary>
    public enum VerifyOrderState : short
    {
        Unknown = 0,  // 不确定
        Unpaid  = -1, // 确认未支付
        Paid    = 1,  // 确认已支付
    }

    /// <summary>
    /// 剧情标记类型
    /// </summary>
    public enum StorylineMarkerType : short
    {
        PeeperSecret = 1, // 偷窥狂的秘密
    }

    /// <summary>
    /// 虚拟空间入口类型
    /// </summary>
    public enum ContactEntranceType : short
    {
        RecordUtterance = 1, // 语录
        SendFlower      = 2, // 送花
        Story           = 3, // 特殊故事
        ComingSoon      = 4, // 敬请期待
        DressUp         = 5, // 装扮
        Vent            = 6, // 发泄
        WhackAMole      = 7, // 打地鼠
    }

    public enum DressUpType : short
    {
        Cloth      = 1, // 服装
        Expression = 2, // 表情
        Background = 3, // 背景
        Line       = 4, // 台词
    }

    public enum IllustrationType : short
    {
        MainLine        = 1, // 主线
        InteractionRoom = 2, // 虚拟空间
    }

    /// <summary>
    /// 渠道类型
    /// </summary>
    [Flags]
    public enum ChannelId
    {
        Default  = 0xFFFF, // 默认情况
        AppStore = 1 << 0, // 苹果
        TapTap   = 1 << 1, // TapTap
        HaoYou   = 1 << 2, // 好游快爆
        BiliBili = 1 << 3, // B站
        HuaWei   = 1 << 4, // 华为
        ZhangYue = 1 << 5, // 掌阅
        Oppo     = 1 << 6, // Oppo
        For4399  = 1 << 7, // 4399
        Hlm      = 1 << 8, // 官包
        Mmy      = 1 << 9, // 摸摸鱼
    }

    /// <summary>
    /// 广告位类型
    /// </summary>
    public enum AdsUnitType : short
    {
        GCoin = 1, // G元广告
        Hp    = 2, // 小电池广告
    }

    /// <summary>
    /// 购买方式类型
    /// </summary>
    public enum PaymentType : short
    {
        NotSpecified = 0, // 未指定
        Recharge     = 1, // 充值
        Ads          = 2, // 广告
        GCoin        = 3, // G元
        Hp           = 4, // 小电池
    }

    /// <summary>
    /// 补丁应用时机
    /// </summary>
    public enum PatchTiming : short
    {
        NotSpecified = 0, // 未指定

        OnGameSceneLoaded = 11, // 进入游戏后
    }

    /// <summary>
    /// UiDialog显示层级
    /// </summary>
    public enum UiDialogDisplayLevel : short
    {
        Desktop        = 0, // 默认级别
        AboveSceneChat = 1, // 在SceneChat之上
    }

    /// <summary>
    /// 当前环境
    /// </summary>
    public enum AppEnvironment : short
    {
        Development = 0, // 开发环境
        Production  = 1, // 生产环境
    }

    /// <summary>
    /// 发泄道具类型
    /// </summary>
    public enum VentPropType : short
    {
        Beat     = 0, // 殴打
        Threaten = 1, // 威胁
    }

    /// <summary>
    /// 有些商品（广告）每天需重置购买次数
    /// </summary>
    public enum PurchaseCountResetType : short
    {
        NeverReset = 0, // 从不重置
        ResetByDay = 1, // 每天重置
    }

    /// <summary>
    /// 更新邮件状态类型
    /// </summary>
    public enum UpdateMailType : byte
    {
        MarkAsRead    = 0, // 标记为已读
        MarkAsClaimed = 1, // 标记为已领取
    }

    /// <summary>
    /// 控制台方便使用
    /// </summary>
    public enum ContactEnum : short
    {
        赵泽宇 = 1,  // 赵泽宇
        赵泽慧 = 2,  // 赵泽慧
        林敏恩 = 3,  // 林敏恩
        煤球  = 4,  // 煤球
        斯黛丽 = 5,  // 斯黛丽
        康康  = 6,  // 康康
        小小  = 7,  // 小小
        哈鲁  = 8,  // 哈鲁
        妈妈  = 9,  // 妈妈
        祁纪  = 10, // 祁纪
        艾拉  = 11, // 艾拉
        小祁  = 12, // 小祁
    }

    /// <summary>
    /// 喵信Actor类型（用在Say/ActionTextClue/ActionImageClue/ShowImage里）
    /// 以区分左边还是右边角色
    /// </summary>
    public enum MeowChatActorType : short
    {
        Left  = 1, // 左边(对方)
        Right = 2, // 右边(自己)
    }

    /// <summary>
    /// 行程类型
    /// </summary>
    public enum ScheduleType : short
    {
        Undefined = 0, // 未定义
        Normal    = 1, // 普通行程
        Story     = 2, // 故事行程
        Gift      = 3, // 礼物行程
    }
}