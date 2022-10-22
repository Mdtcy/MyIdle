/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-09 14:35:12
 * @modify date 2022-09-09 14:35:12
 * @desc [description]
 */

namespace NewLife.BusinessLogic.Signals
{
    public class OnSceneSwitchedSignal
    {
        /// <summary>
        /// 目标场景
        /// </summary>
        public UnityEngine.SceneManagement.Scene DestScene { get; }

        public OnSceneSwitchedSignal(UnityEngine.SceneManagement.Scene scene)
        {
            DestScene = scene;
        }
    }
}