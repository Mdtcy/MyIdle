/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月22日
 * @modify date 2022年10月22日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using Sirenix.OdinInspector;
using UniSwitcher;

namespace Game.Scene
{
    public class SceneController : Switcher
    {
        #region FIELDS

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Button]
        public void EnterGameScene()
        {
            PerformSceneTransition(ChangeScene(Scene.GameScene));
        }

        [Button]
        public void EnterMenuScene()
        {
            PerformSceneTransition(ChangeScene(Scene.FirstScene));
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649