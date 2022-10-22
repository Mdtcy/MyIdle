/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月20日
 * @modify date 2022年10月20日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.UI.MainMenu
{
    public class MainMenuController : BaseUIController<MainMenuView>, IInitializable
    {
        #region FIELDS

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public MainMenuController(MainMenuView tView) : base(tView)
        {
        }

        public void Show()
        {
            View.gameObject.SetActive(true);
            View.Bg.localScale = Vector3.zero;
            View.Bg.DOScale(1, 0.3f).SetEase(Ease.InOutSine);
        }

        public void Hide()
        {
            View.Bg.DOScale(0, 0.3f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                 {
                     View.gameObject.SetActive(false);
                 });
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion

        public void Initialize()
        {
            Show();

            View.BtnHelloWorld.onClick.AddListener(() =>
            {
                Debug.Log("HelloWorld");
            });

            View.BtnStart.onClick.AddListener(() =>
            {
                View.SceneController.EnterGameScene();
            });
        }
    }
}
#pragma warning restore 0649