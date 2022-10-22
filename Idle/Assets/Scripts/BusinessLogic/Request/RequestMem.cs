/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-14 19:41:46
 * @modify date 2018-12-14 19:41:46
 * @desc [仅本地、不涉及存储的请求都放在这里]
 */

using HM;
using HM.GameBase;
using HM.Interface;
using HM.Notification;

namespace NewLife.BusinessLogic.Request
{
    /// <summary>
    /// 包含所有非网络、非存储的业务请求
    /// </summary>
    public class RequestMem : IRequest
    {
        protected readonly Inventory         Inventory;
        private readonly   IItemFactory      factory;
        private readonly   ISendNotification sendNotification;

        protected RequestMem(Inventory inventory, IItemFactory factory, ISendNotification sendNotification)
        {
            Inventory             = inventory;
            this.factory          = factory;
            this.sendNotification = sendNotification;
        }

        /// <inheritdoc />
        public bool HasItem(int itemId)
        {
            return Inventory.HasItem(itemId);
        }

        /// <inheritdoc />
        public virtual void AcquireItems(ItemParams items, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    __AcquireItem(item.ItemId, item.Num);
                }
            }

            onSucc?.Invoke();
        }

        /// <inheritdoc />
        public virtual void AcquireItem(int itemId, int num, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            __AcquireItem(itemId, num);
            onSucc?.Invoke();
        }

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="items">Item列表，包含[ItemId/Num/State]</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        public virtual void RemoveItems(ItemParams items, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogInfo($"[Request]RemoveItems:{items}");

            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    Inventory.RemoveItem(item.ItemId);
                }
            }

            onSucc?.Invoke();
        }

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="num"></param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        public virtual void RemoveItem(int itemId, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogInfo($"[Request]RemoveItem:{itemId}");
            Inventory.RemoveItem(itemId);
            onSucc?.Invoke();
        }

        /// <summary>
        /// 快速登录，如果没有子类派生重写该函数，则为内存模式，每次调用都是注册新用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onSucc"></param>
        /// <param name="onFail"></param>
        public virtual void FastLogin(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            Register(name, onSucc, onFail);
        }

        /// <inheritdoc />
        public virtual void Register(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogDebug("Request::Register");
            onSucc?.Invoke();
        }

        /// <inheritdoc />
        public virtual void Login(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogDebug("Request::Login ");
            onSucc?.Invoke();
        }

        /// <inheritdoc />
        public virtual void Logout(OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogDebug("Request::Logout ");
            onSucc?.Invoke();
        }

        /// <summary>
        /// 保存玩家信息，内存模式下什么也不做
        /// </summary>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        public virtual void Save(OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogDebug("Request::Save ");
            onSucc?.Invoke();
        }

        /// <summary>
        /// 购买物品
        /// </summary>
        /// <param name="itemid">物品Id，必须是可购买的物品（继承自PurchasableConfig）</param>
        /// <param name="num">购买数量</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        public virtual void PurchaseItem(int itemid, int num, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogInfo("Request::PurchaseItem - {0}x{1}", itemid, num);
            onSucc?.Invoke();
        }

        /// <summary>
        /// 消耗物品（比如使用等任何导致物品减少的行为）
        /// </summary>
        /// <param name="itemId">物品Id</param>
        /// <param name="num">消耗的数量</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        public void ConsumeItem(int itemId, int num, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogInfo($"Request::ConsumeItem:{itemId} x {num}");
            __ConsumeItem(itemId, num);
            onSucc?.Invoke();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 私有函数
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void __AcquireItem(int itemId, int num)
        {
            HMLog.LogDebug($"获得物品 {itemId} x {num}");

            if (Inventory.HasItem(itemId))
            {
                var item = Inventory.GetItem(itemId);
                item.Num += num;
            }
            else
            {
                var item = factory.CreateItem(itemId);
                item.ItemId = itemId;
                item.Num    = num;
                item.State  = ItemState.Default;
                Inventory.AddItem(item);
                item.OnPicked();
            }
        }

        private void __ConsumeItem(int itemId, int num)
        {
            var item = Inventory.GetItem(itemId);
            item.Num += -1 * num;
            sendNotification.Publish(NotifyKeys.kOnItemConsumed, itemId);
        }

        /// <summary>
        /// 获得物品奖励
        /// </summary>
        /// <param name="itemId">物品Id</param>
        /// <param name="num">奖励的数量</param>
        /// <param name="scenario">奖励场景，比如是评价任务奖励、每日登录等</param>
        /// <param name="onSucc">成功回调</param>
        /// <param name="onFail">失败回调</param>
        public void BonusItem(int           itemId,
                              int           num,
                              string        scenario,
                              OnRequestSucc onSucc = null,
                              OnRequestFail onFail = null)
        {
            __AcquireItem(itemId, num);
            onSucc?.Invoke();
        }

        public virtual void Reload(OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
        }

        public virtual void Backup(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
        }

        public virtual void FlushToDisk(OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
        }
    }
}