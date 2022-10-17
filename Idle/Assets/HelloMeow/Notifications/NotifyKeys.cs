namespace HM.Notification
{
    public class NotifyKeys
    {
        public static int kOnItemUpdated         = 10001; // fired by 'User', Item初始化不会触发该事件
        public static int kOnItemPicked          = 10002; // 首次获得item时会发送此消息（itemid）
        public static int kOnItemRemoved         = 10003; // 首次获得item时会发送此消息（itemid）
        public static int kOnGameIsReady         = 10004; // 用户登录、数据准备好后发送该消息
        public static int kOnItemConsumed        = 10005; // 当某个item被使用时
        public static int kSaveUserData          = 10006; // 调用Request.Save()方法会发送该消息
        public static int kForceSaveUserData     = 10007; // 强制保存数据数据到本地
        public static int kOnApplicationPause    = 10008; // 当游戏暂停/恢复
    }
}