/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-04-06 19:33:25
 * @modify date 2022-04-06 19:33:25
 * @desc [带缓存的可寻址加载器，适合一个频繁加载/卸载相同资源的UI]
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#pragma warning disable 0649
namespace HM.Addressable
{
    public class AddressableCacheLoader : MonoBehaviour
    {
        #region FIELDS

        // 保存所有加载过资源的handle，统一卸载资源
        private readonly List<AsyncOperationHandle> handles = new List<AsyncOperationHandle>();

        // 保存加载过的资源，不需要重复反复加载
        private readonly Dictionary<object, Sprite> cache = new Dictionary<object, Sprite>();

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 异步加载指定reference
        /// </summary>
        /// <param name="assetRef"></param>
        /// <param name="onLoaded"></param>
        public void LoadAsync(AssetReference assetRef, Action<Sprite, bool> onLoaded)
        {
            if (!assetRef.RuntimeKeyIsValid())
            {
                return;
            }

            // 如果缓存有，则直接返回缓存的资源
            if (cache.ContainsKey(assetRef.RuntimeKey))
            {
                onLoaded?.Invoke(cache[assetRef.RuntimeKey], true);
                return;
            }

            var handle = assetRef.LoadAsyncV1<Sprite>(gameObject, v =>
            {
                // 加载成功后保存到缓存里
                if (!cache.ContainsKey(assetRef.RuntimeKey))
                {
                    cache[assetRef.RuntimeKey] = v;
                }

                onLoaded?.Invoke(v, false);
            });

            handles.Add(handle);
        }

        /// <summary>
        /// 清理缓存
        /// </summary>
        public void ClearCache()
        {
            foreach (var handle in handles)
            {
                handle.Release();
            }
            handles.Clear();

            cache.Clear();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC FIELDS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649