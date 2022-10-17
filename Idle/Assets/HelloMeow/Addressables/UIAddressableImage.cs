/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-04-06 21:35:53
 * @modify date 2022-04-06 21:35:53
 * @desc [description]
 */

using System;
using HM.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

#pragma warning disable 0649
namespace HM.Addressable
{
    public class UIAddressableImage : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Image target;

        private bool setNativeSize;

        private Action<bool> onLoaded;

        #endregion

        #region PROPERTIES

        public Image Target => target;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 异步加载可寻址资源并设置到目标Image上，加载成功后调用onLoaded
        /// </summary>
        /// <param name="assetReference"></param>
        /// <param name="setNativeSize"></param>
        /// <param name="onLoaded">true:使用的是cache，相当于同步加载；false:异步加载</param>
        public virtual void LoadAsync(AssetReference assetReference,
                                      bool           setNativeSize = true,
                                      Action<bool>   onLoaded      = null)
        {
            this.setNativeSize = setNativeSize;
            this.onLoaded      = onLoaded;
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        public virtual void Unload()
        {
            onLoaded = null;
        }

        #endregion

        #region PROTECTED METHODS

        protected void ResetTarget()
        {
            target.sprite = null;

            if (setNativeSize)
            {
                var rt = target.rectTransform;
                rt.sizeDelta = new Vector2(0, rt.sizeDelta.y);
            }
            else
            {
                target.SetAlpha(0);
            }
        }

        protected void SetSpriteToTarget(Sprite result)
        {
            target.sprite = result;
            if (setNativeSize)
            {
                target.SetNativeSize();
            }
            else
            {
                target.SetAlpha(255);
            }
        }

        protected void Notify(bool cacheUsed)
        {
            onLoaded?.Invoke(cacheUsed);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC FIELDS

        #endregion

        #region STATIC METHODS

        #endregion

        [Button("自动获取图片", ButtonSizes.Medium)]
        private void AutoSetImage()
        {
            target = GetComponent<Image>();
            HMLog.Assert(target != null);
        }


    }
}
#pragma warning restore 0649