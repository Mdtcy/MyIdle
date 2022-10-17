namespace HM.GameBase
{
    public interface IItemUpdater : IItemGetter
    {
        /// <summary>
        /// 获得指定物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="num"></param>
        void AddItem(int itemId, int num);

        /// <summary>
        /// 获得物品
        /// </summary>
        /// <param name="itemParams"></param>
        void Acquire(ItemParams itemParams);

        /// <summary>
        /// 消耗指定物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="num"></param>
        void ConsumeItem(int itemId, int num);

        /// <summary>
        /// 获取指定Item，如果没有，则创建一个
        /// </summary>
        /// <param name="itemId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOrAddItem<T>(int itemId) where T : ItemBase;
    }
}