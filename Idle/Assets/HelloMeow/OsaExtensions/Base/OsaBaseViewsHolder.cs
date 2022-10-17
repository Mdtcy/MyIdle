/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-11-25 14:11:30
 * @modify date 2020-11-25 14:11:30
 * @desc [在此拿到model数据更新UI]
 */

using System;
using Com.TheFallenGames.OSA.Core;
using UnityEngine;

namespace HM.OsaExtensions
{
    public abstract class OsaBaseViewsHolder : BaseItemViewsHolder
    {
        private OsaBaseCell baseCell;

        /// <inheritdoc />
        public override void CollectViews()
        {
            base.CollectViews();

            baseCell = root.GetComponent<OsaBaseCell>();

            if (baseCell != null)
            {
                baseCell.ViewsHolder = this;
                baseCell.ItemIndex   = ItemIndex;
            }
        }

        /// <summary>
        /// 更新UI
        /// </summary>
        /// <param name="model"></param>
        public virtual void UpdateViews(OsaBaseModel model)
        {
            if (baseCell != null)
            {
                baseCell.ViewsHolder = this;
                baseCell.ItemIndex   = ItemIndex;
            }
        }

        /// <summary>
        /// 创建UI对象的代理
        /// </summary>
        public Func<GameObject, Transform, Transform> InstantiateDelegate;

        /// <summary>
        /// 是否支持显示指定model类型的数据
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public abstract bool CanPresentModelType(Type modelType);

        /// <summary>
        /// 即将回收或销毁指定ViewsHolder前，会调用该方法
        /// </summary>
        /// <param name="newItemIndex"></param>
        public virtual void OnBeforeRecycleOrDisable(int newItemIndex)
        {
        }

        /// <inheritdoc />
        public override void Init(GameObject    rootPrefabGO,
                                  RectTransform parent,
                                  int           itemIndex,
                                  bool          activateRootGameObject = true,
                                  bool          callCollectViews       = true)
        {
            root = InstantiateDelegate(rootPrefabGO, parent) as RectTransform;

            if (activateRootGameObject)
            {
                root.gameObject.SetActive(true);
            }

            ItemIndex = itemIndex;

            if (callCollectViews)
            {
                CollectViews();
            }
        }
    }
}