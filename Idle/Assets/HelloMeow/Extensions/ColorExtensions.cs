using UnityEngine;
using UnityEngine.UI;

namespace HM.Extensions
{
    public static class ColorExtensions
    {
        public static void SetAlpha(this SpriteRenderer sp, int alpha)
        {
            var c = sp.color;
            c.a = Mathf.Min(alpha, 1);
            sp.color = c;
        }

        public static void SetAlpha(this Image sp, int alpha)
        {
            var c = sp.color;
            c.a = Mathf.Min(alpha, 1);
            sp.color = c;
        }

        public static float GetAlpha(this Image img)
        {
            return img != null ? img.color.a : 0;
        }

        public static void SetAlpha(this Image sp, float alpha)
        {
            var c = sp.color;
            c.a = Mathf.Min(alpha, 1);
            sp.color = c;
        }

        public static void SetAlpha(this Graphic txt, float alpha)
        {
            var c = txt.color;
            c.a       = Mathf.Min(alpha, 1);
            txt.color = c;
        }
    }
}