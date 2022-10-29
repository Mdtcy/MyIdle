/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-14 19:58:44
 * @modify date 2018-12-14 19:58:44
 * @desc [业务请求接口类]
 */

using HM.GameBase;

namespace HM.Interface
{
    /// <summary>
    /// 请求成功回调
    /// </summary>
    public delegate void OnRequestSucc();

    /// <summary>
    /// 请求失败回调
    /// </summary>
    /// <param name="errcode">错误代码</param>
    /// <param name="errmsg">错误消息</param>
    public delegate void OnRequestFail(int errcode, string errmsg);

    /// <summary>
    /// 业务请求接口
    /// </summary>
    public interface IRequest
    {

        /// <summary>
        /// 是否拥有指定物品
        /// </summary>
        /// <param name="itemId"></param>
        bool HasItem(int itemId);

        /// <summary>
        /// 获得物品（累加）
        /// </summary>
        /// <param name="items">Item列表，包含[ItemId/Num/State]</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        void AcquireItems(ItemParams items, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 获得物品（累加）
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="num"></param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        void AcquireItem(int itemId, int num, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="items">Item列表，包含[ItemId/Num/State]</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        void RemoveItems(ItemParams items, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="num"></param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        void RemoveItem(int itemId, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 购买物品
        /// </summary>
        /// <param name="itemId">物品Id，必须是可购买的物品（继承自PurchasableConfig）</param>
        /// <param name="num">购买数量</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        void PurchaseItem(int itemId, int num, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 消耗物品（比如使用等任何导致物品减少的行为）
        /// </summary>
        /// <param name="itemId">物品Id</param>
        /// <param name="num">消耗的数量</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        void ConsumeItem(int itemId, int num, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 获得物品奖励
        /// </summary>
        /// <param name="itemId">物品Id</param>
        /// <param name="num">奖励的数量</param>
        /// <param name="scenario">奖励场景，比如是评价任务奖励、每日登录等</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        void BonusItem(int itemId, int num, string scenario, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 重新加载游戏
        /// </summary>
        /// <param name="onSucc"></param>
        /// <param name="onFail"></param>
        void Reload(OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 备份游戏
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onSucc"></param>
        /// <param name="onFail"></param>
        void Backup(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null);

        /// <summary>
        /// 保存游戏到磁盘
        /// </summary>
        /// <param name="onSucc"></param>
        /// <param name="onFail"></param>
        void FlushToDisk(OnRequestSucc onSucc = null, OnRequestFail onFail = null);
    }
}