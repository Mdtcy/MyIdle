/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-28 21:03:24
 * @modify date 2020-03-28 21:03:24
 * @desc [description]
 */

using HM.Defined;
using HM.Extensions;
using UnityEngine;

namespace HM.Assitance
{
    public static class HmHelper
    {
        /// <summary>
        /// 根据AnchorPoint类型返回对应的数值
        /// </summary>
        /// <param name="apType"></param>
        /// <returns></returns>
        public static Vector2 AnchorPointByAnchorType(AnchorPointType apType)
        {
            switch (apType)
            {
                default:
                case AnchorPointType.Center: return RectTransformExtensions.AP_CC3;
                case AnchorPointType.CenterLeft: return RectTransformExtensions.AP_CL3;
                case AnchorPointType.CenterRight: return RectTransformExtensions.AP_CR3;
                case AnchorPointType.BottomLeft: return RectTransformExtensions.AP_BL3;
                case AnchorPointType.BottomCenter: return RectTransformExtensions.AP_BC3;
                case AnchorPointType.BottomRight: return RectTransformExtensions.AP_BR3;
                case AnchorPointType.TopLeft: return RectTransformExtensions.AP_TL3;
                case AnchorPointType.TopCenter: return RectTransformExtensions.AP_TC3;
                case AnchorPointType.TopRight: return RectTransformExtensions.AP_TR3;
            }
        }
    }
}