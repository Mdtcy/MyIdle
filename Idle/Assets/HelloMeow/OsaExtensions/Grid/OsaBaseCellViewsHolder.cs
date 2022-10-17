/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-12-27 20:12:23
 * @modify date 2020-12-27 20:12:23
 * @desc [description]
 */

using System;
using Com.TheFallenGames.OSA.CustomAdapters.GridView;
using UnityEngine;

#pragma warning disable 0649
namespace HM.OsaExtensions
{
    public abstract class OsaBaseCellViewsHolder : CellViewsHolder
    {
        /// <summary>
        /// 更新UI
        /// </summary>
        /// <param name="model"></param>
        public abstract void UpdateViews(OsaBaseModel model);

        /// <summary>
        /// 重置或销毁前调用该方法，在此函数中可以做ui的清理工作
        /// </summary>
        public abstract void OnBeforeRecycledOrDisabled();

        /// <summary>
        /// 创建UI对象的代理
        /// </summary>
        public Func<GameObject, Transform, Transform> InstantiateDelegate;

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

            this.ItemIndex = itemIndex;

            if (callCollectViews)
            {
                CollectViews();
            }
        }
    }
}
#pragma warning restore 0649