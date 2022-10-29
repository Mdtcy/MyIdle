using HM.GameBase;

namespace HM.Interface
{
    public interface IItemFactory
    {
        /// <summary>
        /// 创建ItemBase
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        ItemBase CreateItem(int itemId);
    }
}