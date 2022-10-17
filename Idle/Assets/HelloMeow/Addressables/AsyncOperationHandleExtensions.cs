/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-04-01 11:29:35
 * @modify date 2022-04-01 11:29:35
 * @desc [description]
 */

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HM.Addressable
{
    public static class AsyncOperationHandleExtensions
    {
        /// <summary>
        /// 释放对应的资源
        /// </summary>
        /// <param name="handle"></param>
        public static void Release(this AsyncOperationHandle handle)
        {
            if (handle.IsValid() && handle.Result != null)
            {
                Addressables.Release(handle);
            }
        }
    }
}