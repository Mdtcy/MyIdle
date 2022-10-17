#pragma warning disable 0649
using System;
using HM.Date;
using HM.Interface;
using HM.Notification;
using UnityEngine;
#if HM_ZENJECT
using Zenject;
#endif

namespace HM.GameBase
{
    [Serializable]
    public class ItemBase : CommonBase
    {
        #region FIELDS
        [SerializeField]
        // 对于需要重写属性的需求，设为protected
        protected int _num;

        [SerializeField]
        // 对于需要重写属性的需求，设为protected
        protected bool _isDirty;

        // 如果不支持zenject，可以自己设置ConfigGetter
        #if HM_ZENJECT
        [Inject]
        #endif
        protected IConfigGetter configGetter;

        #if HM_ZENJECT
        [Inject]
        #endif
        private IItemRemover itemRemover;

    #if HM_ZENJECT
        [Inject]
    #endif
        private IDateTime dateTime;

#if HM_ZENJECT
        [Inject]
#endif
        protected ISendNotification sendNotification;

        [SerializeField]
        private int _itemid;

        [SerializeField]
        protected long _pickedTimeStamp;

        /// <summary>
        /// 获得该item的时间戳
        /// </summary>
        public long PickedTimeStamp
        {
            get => _pickedTimeStamp;
            private set => _pickedTimeStamp = value;
        }

        /// <summary>
        /// 获得后玩家是否查看过该item
        /// </summary>
        public virtual bool IsDirty
        {
            get => _isDirty;
            set => _isDirty = value;
        }
        #endregion

        #region PROPERTIES
        public int ItemId
        {
            get => _itemid;
            set
            {
                _itemid = value;
                OnItemIdSet();
            }
        }
        public virtual int Num
        {
            get => _num;
            set
            {
                if (_num != value)
                {
                    int curr = value;
                    int prev = _num;
                    _num = value;
                    OnNumChangedInternal(prev, curr);
                }
            }
        }
        public ItemState State;
        public string Name => GetBaseConfig() != null ? GetBaseConfig().Name : ItemId.ToString();

        #endregion

        #region PUBLIC METHODS
        private BaseConfig GetBaseConfig()
        {
            return configGetter.GetConfig<BaseConfig>(_itemid);
        }
        public T GetConfig<T>()where T : BaseConfig
        {
            return configGetter.GetConfig<T>(_itemid);
        }

        public T ToType<T>()where T : ItemBase
        {
            return this as T;
        }

        public ItemBase()
        {}

        public ItemBase(int itemId)
        {
        }

        public virtual void Setup(int itemId)
        {
            ItemId      = itemId;
            State       = ItemState.Default;
            _isDisposed = false;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[ItemBase itemId={ItemId},num={Num},state={State},name={Name}]";
        }

		/// <summary>
		/// 短介绍
		/// </summary>
		/// <returns></returns>
        public virtual string ShortDesc()
        {
            return GetConfig<BaseConfig>().ShortDesc();
        }

        /// <summary>
        /// Call this method when accquire this item for the very first time.
        /// </summary>
        public virtual void OnPicked()
        {
            HMLog.LogInfo("[ItemBase]拾取到新的Item:{0}", this);
			PickedTimeStamp = dateTime.Now().Ticks;
            IsDirty = true;
            sendNotification.Publish(NotifyKeys.kOnItemPicked, this);
        }

        public virtual void OnRemoved()
        {
            HMLog.LogInfo("[ItemBase]移除Item:{0}", _itemid);
            sendNotification.Publish(NotifyKeys.kOnItemRemoved, this);
            Dispose();
        }

        #if HM_ZENJECT
        public virtual void OnWillInject(DiContainer container)
        {
            container.Inject(this);
        }
        #endif

        /// <summary>
        /// Item被加载后会收到该函数的调用
        /// </summary>
        public virtual void OnWillLoad()
        {}

        /// <summary>
        /// Item被加载后会收到该函数的调用
        /// </summary>
        public virtual void OnLoaded()
        {}

        /// <summary>
        /// Item即将被持久化保存前调用该方法
        /// </summary>
        public virtual void OnWillSave()
        {}

        /// <summary>
        /// 自检
        /// </summary>
        /// <returns></returns>
        public virtual bool SelfCheck()
        {
            if (GetConfig<BaseConfig>() == null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region PROTECTED METHODS
        protected virtual void OnItemIdSet()
        {
            // !! 在这个函数内不要做任何修改序列化变量或属性的操作，会被覆盖
        }

        // 将自己从inventory中删除
        protected void RemoveMe()
        {
            itemRemover.RemoveItem(ItemId);
        }

        protected virtual void OnNumChangedInternal(int prev, int curr)
        {
        }
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion
    }

    [Serializable]
    public class ItemBase<T> : ItemBase where T : BaseConfig
    {
        public virtual T Config => configGetter.GetConfig<T>(ItemId);

        public ItemBase() {}

        public ItemBase(int itemId) : base(itemId) {}
    }
}
#pragma warning restore 0649