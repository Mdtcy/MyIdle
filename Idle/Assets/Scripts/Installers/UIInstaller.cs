/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月20日
 * @modify date 2022年10月20日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using _Game.Scripts.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace NewLife.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public GameObject MainViewPrefab;

        public override void InstallBindings()
        {
            // MainMenu
            Container.Bind<MainMenuView>().FromComponentInNewPrefab(MainViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuController>().AsSingle();
        }
    }
}
#pragma warning restore 0649