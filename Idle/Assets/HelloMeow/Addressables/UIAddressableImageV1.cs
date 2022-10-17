/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-04-02 15:28:23
 * @modify date 2022-04-02 15:28:23
 * @desc [加载可寻址图片]
 */

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#pragma warning disable 0649
namespace HM.Addressable
{
    /// <summary>
    /// 该类有个问题，如果连续快速加载/卸载资源，比如在对话树频繁切换相同的节点，可能第一次的图片尚未加载成功，就开始加载第二张了
    /// 那么第一次的handle就丢失了，不会被释放
    /// </summary>
    public class UIAddressableImageV1 : UIAddressableImage
    {
        #region FIELDS

        [LabelText("加载时是否重置Image")]
        [SerializeField]
        private bool resetTargetOnLoad = true;

        private AsyncOperationHandle handle;

        private AssetReference assetRef;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <inheritdoc />
        public override void LoadAsync(AssetReference assetReference,
                                       bool           setNativeSize = true,
                                       Action<bool>   onLoaded      = null)
        {
            base.LoadAsync(assetReference, setNativeSize, onLoaded);

            if (assetReference == assetRef)
            {
                return;
            }

            Unload();

            assetRef = assetReference;

            if (resetTargetOnLoad)
            {
                ResetTarget();
            }

            if (!assetReference.RuntimeKeyIsValid())
            {
                return;
            }

            handle = assetReference.LoadAsyncV1<Sprite>(gameObject, OnSpriteLoaded);
        }

        /// <inheritdoc />
        public override void Unload()
        {
            handle.Release();
            handle = default;
            assetRef = null;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnSpriteLoaded(Sprite result)
        {
            SetSpriteToTarget(result);
            Notify(false);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649