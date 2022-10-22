/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date 14:15:08
 * @modify date 14:15:08
 * @desc [判断UI是否显示在了屏幕上]
 */

using UnityEngine;

namespace NewLife.UI.Extensions
{
    /// <summary>
    /// 判断UI是否显示在了屏幕上
    /// </summary>
    public static class RectTransformExtension
    {
        #region PUBLIC METHODS

        /// <summary>
        /// 是否完全的显示在屏幕上
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera = null)
        {
            if (!rectTransform.gameObject.activeInHierarchy)
            {
                return false;
            }

            return CountCornersVisibleFrom(rectTransform, camera) == 4;
        }

        /// <summary>
        /// 是否有部分显示在屏幕上
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera = null)
        {
            if (!rectTransform.gameObject.activeInHierarchy)
            {
                return false;
            }

            return CountCornersVisibleFrom(rectTransform, camera) > 0;
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// 获取有多少个corners是可见的
        /// </summary>
        /// <returns>int</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Overlay Canvasses则设置camera为null即可</param>
        private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera = null)
        {
            Rect      screenBounds  = new Rect(0f, 0f, Screen.width, Screen.height);
            Vector3[] objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            int     visibleCorners = 0;
            Vector3 tempScreenSpaceCorner;

            for (var i = 0; i < objectCorners.Length; i++)
            {
                if (camera != null)
                {
                    tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]);
                }
                else
                {
                    // 如果没有则视为canvas 是 Overlay 而且 world space == screen space
                    tempScreenSpaceCorner = objectCorners[i];
                }

                // 如果corner在屏幕上
                if (screenBounds.Contains(tempScreenSpaceCorner))
                {
                    visibleCorners++;
                }
            }

            return visibleCorners;
        }

        #endregion
    }
}