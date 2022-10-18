/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2020-12-29 19:22:39
 * @desc [项目安装器]
 */

using HelloMeow.Signal;
using HM;
using HM.GameBase;
using HM.Interface;
using HM.Notification;
using NewLife.BusinessLogic.Archive;
using NewLife.BusinessLogic.DateTimeUtils;
using NewLife.BusinessLogic.Item;
using NewLife.BusinessLogic.Request;
using NewLife.Config;
using NewLife.Config.Helper;
using NewLife.UI.AudioManager;
using NewLife.UI.SRDebuggers;
using UnityEngine;
using Zenject;

#pragma warning disable 0649

namespace NewLife.Installers
{
    /// <summary>
    /// 项目安装器
    /// </summary>
    public class ProjectInstaller : MonoInstaller
    {
        #region FIELDS

        // // 播放音效
        // [SerializeField]
        // private EffectPlayer effectPlayer;
        //
        // // 播放背景音乐
        // [SerializeField]
        // private BackgroundMusicPlayer backgroundMusicPlayer;
        //
        // // 播放环境音效
        // [SerializeField]
        // private AmbientAudioPlayer ambientAudioPlayer;
        //
        // // 播放Npc音效
        // [SerializeField]
        // private NpcAudioPlayer npcAudioPlayer;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <inheritdoc />
        public override void InstallBindings()
        {
            // Signal
            DeclareSignals();

            // // Players
            // Container.BindInterfacesTo<EffectPlayer>().FromInstance(effectPlayer).AsCached();
            // Container.BindInterfacesTo<BackgroundMusicPlayer>().FromInstance(backgroundMusicPlayer).AsCached();
            // Container.BindInterfacesTo<AmbientAudioPlayer>().FromInstance(ambientAudioPlayer).AsCached();
            // Container.BindInterfacesTo<NpcAudioPlayer>().FromInstance(npcAudioPlayer);

            //Config
            Container.Bind<ConfigContainer>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ConfigCollectionFactory>().AsSingle();
            Container.Bind<IConfigGetter>().To<ConfigGetter>().AsSingle();

            // notificationCenter
            Container.BindInterfacesAndSelfTo<NotificationCenter>().FromInstance(NotificationCenter.Instance);

            // storage
            Container.Bind<StoreMem>().AsSingle(); // kv
            Container.BindInterfacesAndSelfTo<EasySaveArchive>().AsSingle();


            // request
            Container.Bind<IRequest>().To<RequestLocal>().AsSingle();
            Container.Bind<RequestLocal>()
                     .FromSubContainerResolve()
                     .ByMethod(InstallRequestLocalWithOptionalSaveState)
                     .AsCached();

            // inventory
            Container.BindInterfacesAndSelfTo<PersistInventory>().AsSingle();
            Container.Bind<Inventory>().AsSingle();

            // Item
            Container.Bind<IConfigChecker>().To<ConfigChecker>().AsSingle();
            Container.Bind<IItemFactory>().To<ItemFactory>().AsSingle();
            Container.Bind<IItemGetter>().To<ItemGetter>().AsSingle();
            Container.Bind<IItemUpdater>().To<ItemUpdater>().AsSingle();
            Container.Bind<IItemRemover>().To<ItemRemover>().AsSingle();
            Container.Bind<IItemListGetter>().To<ItemListGetter>().AsSingle();
            Container.Bind(typeof(ItemsGetter<>)).AsCached();

            // SROptions
            SROptions.CreateInstanceDelegate = CreateSROptionsInstance;

            // StartDate
            Container.Bind<StartDate>().AsSingle();

            // SerializableConfig
            Container.Bind(typeof(SerializableConfig<>)).AsTransient();
            Container.Bind<SerializableConfigFactory>().AsCached();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private SROptions CreateSROptionsInstance()
        {
            var inst = new SROptionsNewLife();
            Container.Inject(inst);

            return inst;
        }

        private void InstallRequestLocalWithOptionalSaveState(DiContainer subContainer)
        {
            subContainer.Bind<RequestLocal>().AsCached();
        }

        // 声明信号
        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<OnInventoryLoadedSignal>();
            Container.DeclareSignal<OnUserLoginSignal>();
        }

        #endregion

        #region STATIC FIELDS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649