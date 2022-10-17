/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-04-13 16:04:48
 * @modify date 2021-04-13 16:04:48
 * @desc [扩展AssetReference功能，提供更方便、可调试的方法]
 */

using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HM.Addressable
{
    public static class AssetReferenceExtensions
    {
        /// <summary>
        /// 是否是相同的资源
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="another"></param>
        /// <returns></returns>
        public static bool IsSameAsset(this AssetReference reference, AssetReference another)
        {
            if (reference == null || another == null)
            {
                return false;
            }

            return reference.RuntimeKey == another.RuntimeKey;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="onLoaded"></param>
        /// <param name="onFailed"></param>
        /// <typeparam name="T"></typeparam>
        public static AsyncOperationHandle LoadAsyncV1<T>(this AssetReference reference,
                                                          GameObject attachedGameObject,
                                                          Action<T>           onLoaded,
                                                          Action              onFailed = null) where T : class
        {
            if (!reference.RuntimeKeyIsValid())
            {
                HMLog.LogWarning($"[AssetReference]AssetReference的RuntimeKey无效:{reference}");
                onFailed?.Invoke();

                return default;
            }

            // HMLog.LogDebug($"[AssetReference]加载资源:{reference.RuntimeKey}");
            var handle = Addressables.LoadAssetAsync<T>(reference.RuntimeKey);
            ILoadingAsset(handle, reference.RuntimeKey, onLoaded, onFailed).CancelWith(attachedGameObject).RunCoroutine(attachedGameObject);
            return handle;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="onLoaded"></param>
        /// <param name="onFailed"></param>
        /// <typeparam name="T"></typeparam>
        public static AsyncOperationHandle LoadAsyncV2<T>(this AssetReference reference,
                                                          GameObject attachedGameObject,
                                                          Action<T>           onLoaded,
                                                          Action              onFailed = null) where T : class
        {
            if (!reference.RuntimeKeyIsValid())
            {
                HMLog.LogWarning($"[AssetReference]AssetReference的RuntimeKey无效:{reference}");
                onFailed?.Invoke();

                return default;
            }

            // HMLog.LogDebug($"[AssetReference]加载资源:{reference.RuntimeKey}");

            if (reference.IsValid())
            {
                // 在加载中，等待加载完成
                var handle = reference.OperationHandle.Convert<T>();
                ILoadingAsset(handle, reference.RuntimeKey, onLoaded, onFailed).CancelWith(attachedGameObject).RunCoroutine(attachedGameObject);

                return handle;
            }
            else
            {
                var handle = reference.LoadAssetAsync<T>();
                ILoadingAsset(handle, reference.RuntimeKey, onLoaded, onFailed).CancelWith(attachedGameObject).RunCoroutine(attachedGameObject);

                return handle;
            }
        }

        private static IEnumerator<float> ILoadingAsset<T>(AsyncOperationHandle<T> handle, object runtimeKey, Action<T> onLoaded = null,  Action onFailed = null)
        {
            while (!handle.IsDone)
            {
                yield return Timing.WaitForOneFrame;
            }

            if (handle.IsValid() && handle.Status == AsyncOperationStatus.Succeeded)
            {
                HMLog.LogVerbose($"[AssetReference]加载资源成功，地址为:{runtimeKey}，资源为:{handle.Result}");
                onLoaded?.Invoke(handle.Result);
            }
            else
            {
                HMLog.LogWarning($"[AssetReference]加载资源失败，runtimeKey:{runtimeKey} / isValid:{handle.IsValid()}");
                onFailed?.Invoke();
            }
        }

        /// <summary>
        /// 加载资源，返回句柄，可以更细粒度控制加载流程
        /// </summary>
        /// <param name="reference"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AsyncOperationHandle LoadAssetAsyncManuallyHandled<T>(this AssetReference reference) where T : class
        {
            if (!reference.RuntimeKeyIsValid())
            {
                HMLog.LogWarning($"[AssetReference]AssetReference的RuntimeKey无效:{reference}");
                return default;
            }

            HMLog.LogVerbose($"[AssetReference]手动加载资源:[{reference.RuntimeKey}]");

            return reference.IsValid() ? reference.OperationHandle : reference.LoadAssetAsync<T>();
        }
    }
}