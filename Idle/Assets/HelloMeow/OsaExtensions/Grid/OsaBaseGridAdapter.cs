/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-12-27 20:12:13
 * @modify date 2020-12-27 20:12:13
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.CustomAdapters.GridView;

#pragma warning disable 0649
namespace HM.OsaExtensions
{
    /// <summary>
    /// 限制：
    /// * 只能有一种prefab
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TCellViewsHolder"></typeparam>
    public abstract class OsaBaseGridAdapter<TModel, TParams, TCellViewsHolder> : GridAdapter<TParams, TCellViewsHolder>
        where TModel : OsaBaseModel
        where TParams : OsaBaseGridParams
        where TCellViewsHolder : OsaBaseCellViewsHolder, new()
    {
        #region FIELDS

        private List<TModel> models = new List<TModel>(32);

        #endregion

        #region PROPERTIES

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
        /// 刷新显示Grid
        /// </summary>
        /// <param name="models"></param>
        public void RefreshItems(List<TModel> models)
        {
            this.models.Clear();
            this.models.AddRange(models);

            if (IsInitialized)
            {
                Refresh();
            }
            else
            {
                Initialized += OnOsaInitialized;
            }
        }

        /// <inheritdoc />
        public override void Refresh(bool contentPanelEndEdgeStationary = false /*ignored*/, bool keepVelocity = false)
		{
			_CellsCount = models.Count;
			base.Refresh(false, keepVelocity);
		}

        /// <inheritdoc />
        protected override void UpdateCellViewsHolder(TCellViewsHolder viewsHolder)
        {
            viewsHolder.UpdateViews(models[viewsHolder.ItemIndex]);
        }

        /// <inheritdoc />
        protected override void OnBeforeRecycleOrDisableCellViewsHolder(TCellViewsHolder viewsHolder, int newItemIndex)
        {
            base.OnBeforeRecycleOrDisableCellViewsHolder(viewsHolder, newItemIndex);
            viewsHolder.OnBeforeRecycledOrDisabled();
        }

        /// <inheritdoc />
        protected override CellGroupViewsHolder<TCellViewsHolder> GetNewCellGroupViewsHolder()
        {
            return new OsaBaseCellGroupViewsHolder<TCellViewsHolder>() {Pool = _Params.Pool};
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnOsaInitialized()
        {
            Initialized -= OnOsaInitialized;
            Refresh();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649