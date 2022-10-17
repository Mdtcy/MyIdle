namespace HM.Interface
{
    public interface IGameSave
    {
        /// <summary>
        /// 保存游戏进度
        /// </summary>
        void Save();
        /// <summary>
        /// 保存游戏进度到磁盘
        /// </summary>
        void FlushToDisk();
    }
}