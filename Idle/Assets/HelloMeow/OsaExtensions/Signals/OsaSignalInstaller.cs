/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-11-10 16:11:21
 * @modify date 2021-11-10 16:11:21
 * @desc [需以GameObjectContext的方式绑定该Installer，避免放在SceneContext里
 * 否则如果同时有多个osa显示，会导致多个osa收到同一个消息]
 */

using Zenject;

namespace HM.OsaExtensions
{
    public class OsaSignalInstaller : MonoInstaller
    {
        /// <inheritdoc />
        public override void InstallBindings()
        {
            DeclareSignals();
            SignalBusInstaller.Install(Container);
        }
        private void DeclareSignals()
        {
            Container.DeclareSignal<OsaRequestSizeChangeSignal>();
        }
    }
}