/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-04-06 17:07:01
 * @modify date 2022-04-06 17:07:01
 * @desc [加载可寻址图片]
 */

using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace HM.Addressable
{
    public class UIAddressableImageV2 : UIAddressableImage
    {
        [Inject]
        private AddressableCacheLoader loader;

        /// <inheritdoc />
        public override void LoadAsync(AssetReference assetReference,
                                       bool           setNativeSize = true,
                                       Action<bool>   onLoaded      = null)
        {
            base.LoadAsync(assetReference, setNativeSize, onLoaded);

            ResetTarget();

            loader.LoadAsync(assetReference, OnSpriteLoaded);
        }

        /// <inheritdoc />
        public override void Unload()
        {
            // loader自动释放
        }

        private void OnSpriteLoaded(Sprite result, bool cached)
        {
            SetSpriteToTarget(result);
            Notify(cached);
        }
    }
}