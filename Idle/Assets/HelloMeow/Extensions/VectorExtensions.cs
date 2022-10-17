using UnityEngine;

namespace HM.Extensions
{
    public static class VectorExtensions
    {
        public static bool WithinBound(this Vector2 vec,int row, int col)
        {
            return vec.x >= 0 && vec.x < row && vec.y >= 0 && vec.y < col;
        }

        /// <summary>
        /// 屏幕几个常用的normalized位置[屏幕左下角为(0,0)，右上角为(1,1)]
        /// </summary>
        public static Vector3 Center = new Vector3(0.5f, 0.5f);
        public static Vector3 CenterLeft = new Vector3(0.0f, 0.5f);
        public static Vector3 CenterRight = new Vector3(1.0f, 0.5f);
        public static Vector3 TopLeft = new Vector3(0.0f, 1.0f);
        public static Vector3 TopCenter = new Vector3(0.5f, 1.0f);
        public static Vector3 TopRight = new Vector3(1.0f, 1.0f);
        public static Vector3 BottomLeft = new Vector3(0.0f, 0.0f);
        public static Vector3 BottomCenter = new Vector3(0.5f, 0.0f);
        public static Vector3 BottomRight = new Vector3(1.0f, 0.0f);

        public static void SetX(this Vector2 v, float x)
        {
                v.Set(x, v.y);
        }

        public static void SetY(this Vector2 v, float y)
        {
            v.Set(v.x, y);
        }

    }
}
