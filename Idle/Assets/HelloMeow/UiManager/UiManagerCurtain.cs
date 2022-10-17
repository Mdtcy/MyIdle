/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-03 12:03:26
 * @modify date 2020-03-03 12:03:26
 * @desc [description]
 */

using DG.Tweening;
using HM.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HM.GameBase
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class UiManagerCurtain : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Button button;

        [SerializeField]
        private Image image;

        private Color color;

        private TweenCallback actionOnFadeInFinish;

        #endregion

        #region PROPERTIES

        private TweenCallback ActionOnFadeInFinish => actionOnFadeInFinish ?? (actionOnFadeInFinish = OnFadeInFinish);

        #endregion

        #region PUBLIC METHODS

        public void Setup(Color curtainColor, UnityAction onButtonClick)
        {
            color = curtainColor;
            image = image ? image : GetComponent<Image>();
            button = button ? button : GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onButtonClick);
        }

        public void FadeIn()
        {
            button.interactable = false;
            image.SetAlpha(image.GetAlpha());
            image.DOKill();
            image.DOColor(color, 0.3f)
                 .SetEase(Ease.Linear)
                 .SetUpdate(true)
                 .SetId(GetInstanceID())
                 .OnComplete(ActionOnFadeInFinish);
        }

        public void SetVisible(bool v)
        {
            image.SetAlpha(v ? 1 : 0);
        }

        public void SetBlock(bool v)
        {
            image.raycastTarget = v;
        }

        public void SetClickable(bool v)
        {
            button.enabled = v;
        }

        public void FadeOut(TweenCallback onFinish)
        {
            image.DOKill();
            image.DOColor(new Color(0, 0, 0, 0), 0.3f)
                 .SetEase(Ease.Linear)
                 .SetUpdate(true)
                 .SetId(GetInstanceID())
                 .OnComplete(onFinish);
        }
        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            image = image ? image : GetComponent<Image>();
            button = button ? button : GetComponent<Button>();
        }

        private void OnFadeInFinish()
        {
            button.interactable = true;
        }
        #endregion

        #region STATIC METHODS

        #endregion
    }
}