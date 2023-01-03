/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月3日
 * @modify date 2023年1月3日
 * @desc []
 */

#pragma warning disable 0649
using Game.FloatingText;
using UnityEngine;
using Zenject;

namespace NewLife.Installers
{
    public class BattleSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private FloatingTextUseDamageNumsPro floatingTextUseDamageNumsPro;

        public override void InstallBindings()
        {
            Container.Bind<IFloatingText>().FromInstance(floatingTextUseDamageNumsPro).AsSingle();
        }
    }
}
#pragma warning restore 0649