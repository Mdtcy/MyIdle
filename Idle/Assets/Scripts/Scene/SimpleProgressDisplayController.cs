/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月22日
 * @modify date 2022年10月22日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using Cysharp.Threading.Tasks;
using UniSwitcher;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scene
{
    public class SimpleProgressDisplayController : ProgressDisplayController
    {
        #region FIELDS

        [SerializeField]
        private Slider progressBar;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public override void SetProgress(float progress)
        {
            progressBar.value = progress;
        }

        public override void Enable(bool reset = true)
        {
        }

        public override void Disable()
        {
        }

        public override void SetDDoL()
        {
        }

        public override UniTask Close()
        {
            return UniTask.CompletedTask;
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