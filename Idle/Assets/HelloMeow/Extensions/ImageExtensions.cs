using UnityEngine;
using UnityEngine.UI;

namespace HM.Extensions {
	public static class ImageExtensions
	{
        public static void SetSpriteAndFit(this Image image, Sprite sprite)
        {
            image.sprite = sprite;
            image.SetNativeSize();
        }

        public static void SetMaxWidth(this Image image, float maxWidth, bool preserveAspect = true)
        {
            var imageSize = image.sprite.rect;

            var rt = image.rectTransform;

            float scale = maxWidth / imageSize.width;

            rt.SetWidth(Mathf.Min(rt.GetWidth(), maxWidth));

            if (preserveAspect)
            {
                rt.SetHeight(imageSize.height * Mathf.Min(1, scale));
            }
        }
    }
}
