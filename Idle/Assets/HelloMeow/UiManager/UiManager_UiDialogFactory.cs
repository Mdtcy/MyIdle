/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-05 11:03:08
 * @modify date 2020-03-05 11:03:08
 * @desc [description]
 */

using UnityEngine;
#if HM_ZENJECT
using Zenject;
#endif

namespace HM.GameBase
{
    public partial class UiManager
    {
    #if HM_ZENJECT
        public interface IUiDialogFactory : IFactory<GameObject, UiDialog>
        {}

        [Inject]
        public void Construct(IUiDialogFactory factory)
        {
            this.factory = factory;
        }
    #else
        public interface IUiDialogFactory
        {
            UiDialog Create(GameObject prefab);
        }
    #endif

        private IUiDialogFactory factory;

        public IUiDialogFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    HMLog.LogVerbose("UiManager::Use default factory to instantiate dialog");
                    // use default one
                    factory = new UiDialogDefaultFactory();
                }
                return factory;
            }
        }

        // 默认UiDialog工厂
        private class UiDialogDefaultFactory : IUiDialogFactory
        {
            public UiDialog Create(GameObject param)
            {
                HMLog.LogVerbose($"UiManager::Create dialog[{param}] from default factory");
                var go = Instantiate(param);
                return go.GetComponent<UiDialog>();
            }
        }
    }
}