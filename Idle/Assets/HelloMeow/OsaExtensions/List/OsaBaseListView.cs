/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-11-25 14:11:13
 * @modify date 2020-11-25 14:11:13
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Zenject;

#pragma warning disable 0649
namespace HM.OsaExtensions
{
    public abstract class OsaBaseListView<TModel, TParams, TItemViewsHolder> : OSA<TParams, TItemViewsHolder>
        where TModel : OsaBaseModel
        where TParams : OsaBaseListParams
        where TItemViewsHolder : OsaBaseViewsHolder
    {
        #region FIELDS

        private readonly List<TModel> items = new List<TModel>(32);

        public virtual event Action<double> ScrollPositionChangedCalibrated;

        [Inject]
        private SignalBus signalBus;

        #endregion

        #region PROPERTIES

        protected List<TModel> Items => items;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="onInitialized"></param>
        public void Initialize(Action onInitialized)
        {
            if (IsInitialized)
            {
                onInitialized?.Invoke();
            }
            else
            {
                void UnsubscribeOnInitialized()
                {
                    Initialized -= UnsubscribeOnInitialized;
                    onInitialized?.Invoke();
                }

                Initialized += UnsubscribeOnInitialized;
            }
        }

        /// <summary>
        /// 刷新显示
        /// </summary>
        /// <param name="items"></param>
        public void RefreshItems(List<TModel> items, bool contentPanelEndEdgeStationary = false, bool keepVelocity = false)
        {
            HMLog.Assert(IsInitialized);

            this.items.Clear();
            this.items.AddRange(items);

            ResetItems(this.items.Count, contentPanelEndEdgeStationary, keepVelocity);
        }

        /// <summary>
        /// 在指定位置新增item
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="index"></param>
        public void InsertItem(TModel newItem, int index)
        {
            items.Insert(index, newItem);
            InsertItems(index, 1);
        }

        /// <summary>
        /// 追加一个新item
        /// </summary>
        /// <param name="newItem"></param>
        public void AppendItem(TModel newItem, bool contentPanelEndEdgeStationary = false, bool keepVelocity = false)
        {
            items.Add(newItem);
            InsertItems(items.Count - 1, 1, contentPanelEndEdgeStationary, keepVelocity);
        }

        /// <summary>
        /// 新增一组item
        /// </summary>
        /// <param name="newItems"></param>
        /// <param name="index"></param>
        public void InsertItems(List<TModel> newItems, int index, bool contentPanelEndEdgeStationary = false)
        {
            items.InsertRange(index, newItems);
            InsertItems(index, newItems.Count, contentPanelEndEdgeStationary);
        }

        #region SmoothScroll

        /// <summary>
        /// 平稳滚动到指定元素处
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <param name="duration"></param>
        /// <param name="onDone"></param>
        public void SmoothScrollTo(int itemIndex, float duration, Action onDone = null)
        {
            if (itemIndex >= GetItemsCount())
            {
                return;
            }

            SmoothScrollTo(itemIndex, duration, 0, 0, null, onDone);
        }

        /// <summary>
        /// 平稳滚动到第一个元素位置
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onDone"></param>
        public void SmoothScrollToFirst(float duration, Action onDone = null)
        {
            SmoothScrollTo(0, duration, onDone);
        }

        /// <summary>
        /// 平稳滚动到最后一个元素位置
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onDone"></param>
        public void SmoothScrollToLast(float duration, Action onDone = null)
        {
            SmoothScrollTo(items.Count - 1, duration, onDone);
        }

        #endregion

        #region Scroll

        /// <summary>
        /// 直接滚动到第一个元素位置
        /// </summary>
        public void ScrollToFirst()
        {
            ScrollTo(0);
        }

        /// <summary>
        /// 直接滚动到最后一个元素位置
        /// </summary>
        public void ScrollToLast()
        {
            if (items.Count > GetItemsCount() || GetItemsCount() == 0)
            {
                return;
            }

            ScrollTo(items.Count - 1);
        }

        #endregion

        #endregion

        #region PROTECTED METHODS

        protected override void OnScrollPositionChanged(double normPos)
        {
            ScrollPositionChangedCalibrated?.Invoke(IsContentBiggerThanViewport() ? normPos : 0);
        }

        /// <inheritdoc />
        protected override bool ShouldDestroyRecyclableItem(TItemViewsHolder inRecycleBin, bool isInExcess)
        {
            // 默认返回true，将不在显示范围之内的多余的vh资源释放掉，包括调用Destroy(vh.root)
            // 如果使用Pool，则不能允许此种情况发生。
            return false;
        }

        /// <inheritdoc />
        protected override bool IsRecyclable(TItemViewsHolder potentiallyRecyclable,
                                             int              indexOfItemThatWillBecomeVisible,
                                             double           sizeOfItemThatWillBecomeVisible)
        {
            // 如果有多个prefab，复写该方法以判断给定vh是否和model兼容
            var model = items[indexOfItemThatWillBecomeVisible];

            return potentiallyRecyclable.CanPresentModelType(model.CachedType);
        }

        /// <inheritdoc />
        protected override void OnBeforeRecycleOrDisableViewsHolder(TItemViewsHolder inRecycleBinOrVisible,
                                                                    int              newItemIndex)
        {
            inRecycleBinOrVisible.OnBeforeRecycleOrDisable(newItemIndex);
        }

        /// <inheritdoc />
        protected override void OnItemHeightChangedPreTwinPass(TItemViewsHolder vh)
        {
            base.OnItemHeightChangedPreTwinPass(vh);
            items[vh.ItemIndex].IsDirty = false;
        }

        /// <inheritdoc />
        protected override void UpdateViewsHolder(TItemViewsHolder newOrRecycled)
        {
            TModel model = items[newOrRecycled.ItemIndex];
            newOrRecycled.UpdateViews(model);

            if (model.IsDirty)
            {
                newOrRecycled.MarkForRebuild();
                ScheduleComputeVisibilityTwinPass(true);
            }
        }

        /// <inheritdoc />
        protected override void RebuildLayoutDueToScrollViewSizeChange()
        {
            foreach (var model in items)
            {
                model.IsDirty = true;
            }

            base.RebuildLayoutDueToScrollViewSizeChange();
        }

        /// <inheritdoc />
        protected override void DestroyAndClearItemsInList(List<TItemViewsHolder> vhs)
        {
            foreach (var viewsHolder in vhs)
            {
                if (_Params.Pool.IsSpawned(viewsHolder.root))
                {
                    _Params.Pool.Despawn(viewsHolder.root);
                    viewsHolder.root = null;
                }
            }
            base.DestroyAndClearItemsInList(vhs);
        }

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();
            signalBus.Subscribe<OsaRequestSizeChangeSignal>(OnSizeChangeRequest);
        }

        #endregion

        #region PRIVATE METHODS

        private void OnOsaInitialized()
        {
            Initialized -= OnInitialized;
            ResetItems(items.Count);
        }

        private bool IsContentBiggerThanViewport()
        {
            return GetContentSize() > GetViewportSize();
        }

        private void OnSizeChangeRequest(OsaRequestSizeChangeSignal signal)
        {
            int idx = signal.ItemIndex;
            if (idx < Items.Count)
            {
                Items[idx].IsDirty = true;
            }
            ForceUpdateViewsHolderIfVisible(idx);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649