/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月20日
 * @modify date 2022年10月20日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using Game.Scene;
using Zenject;

namespace _Game.Scripts.UI.MainMenu
{
    public class MainMenuController : BaseUIController<MainMenuView>, IInitializable
    {
        #region FIELDS

        [Inject]
        private SceneController sceneController;

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
        }

        public void Hide()
        {
            View.gameObject.SetActive(false);
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
            View.gameObject.SetActive(true);

            View.BtnStart.onClick.AddListener(() =>
            {
                sceneController.EnterGameScene();
            });
        }
    }
}
#pragma warning restore 0649