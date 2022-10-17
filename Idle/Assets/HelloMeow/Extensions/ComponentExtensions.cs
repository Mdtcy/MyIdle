/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-11-14 15:11:08
 * @modify date 2019-11-14 15:11:08
 * @desc [description]
 */

using UnityEngine;

namespace HM.Extensions
{
    public static class ComponentExtensions
    {
		public static RectTransform rectTransform(this Component cp){
			return cp.transform as RectTransform;
		}

		public static float Remap (this float value, float from1, float to1, float from2, float to2) {
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}
    }
}