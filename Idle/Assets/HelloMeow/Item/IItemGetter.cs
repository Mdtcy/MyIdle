namespace HM.GameBase
{
    public interface IItemGetter
    {
        T GetItem<T>(int itemId) where T : ItemBase;
        bool HasItem(int itemId);

        /// <summary>
        /// item数量
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        int ItemCount(int itemId);
    }
}