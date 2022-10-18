/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-09 16:06:54
 * @modify date 2020-06-09 16:06:54
 * @desc [description]
 */

using HM.GameBase;
using UnityEngine;
using Zenject;

namespace NewLife.Installers
{
    public class UiManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactoryCustomInterface<GameObject, UiDialog, UiDialog.Factory, UiManager.IUiDialogFactory>()
                     .FromFactory<PrefabFactory<UiDialog>>();
        }
    }
}