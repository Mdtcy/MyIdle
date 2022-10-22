/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月22日
 * @modify date 2022年10月22日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UniSwitcher.Domain;
namespace DefaultNamespace.Scene
{
    public class Scene : BaseScene
    {
        ///////////////////////////////////////
        // WRITE YOUR SCENE DEFINITION HERE! //
        ///////////////////////////////////////

        private Scene(string rawValue) : base(rawValue)
        {

        }

        public static Scene FirstScene => new Scene("Assets/Scenes/Menu.unity");
        public static Scene GameScene => new Scene("Assets/Scenes/Game.unity");
    }
}
#pragma warning restore 0649