/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-26 14:05:38
 * @modify date 2020-05-26 14:05:38
 * @desc [description]
 */

#pragma warning disable 0649

using HM;
using NewLife.UI.AudioManager;
using PathologicalGames;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace NewLife.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        #region FIELDS

        // // FloatingText池
        // [SerializeField]
        // private SpawnPool floatingTextPool;

        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS
        public override void InstallBindings()
        {
            // Audio
            // Container.BindInterfacesTo<AudioSwitch>().AsCached().NonLazy();

            // FloatingTextPool
            // Container.BindInstance(floatingTextPool).WithId(ZenjectId.FloatingTextPool).AsSingle();

            // so we can inject dependencies into pooled gameObjects and attached scripts.
            UseCustomInstanceDelegateForPoolManager();
        }

        #endregion

        #region PROTECTED METHODS
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC FIELDS
        #endregion

        #region STATIC METHODS

        #endregion

        #region PoolManager

        private void UseCustomInstanceDelegateForPoolManager()
        {
            InstanceHandler.InstantiateDelegates += InstantiateDelegate;
            InstanceHandler.DestroyDelegates     += DestroyDelegate;
        }

        private GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            HMLog.LogVerbose($"PoolManager using custom instantiate delegate for prefab {prefab.name}");

            return Container.InstantiatePrefab(prefab, pos, rot, null);
        }

        private void DestroyDelegate(GameObject instance)
        {
            HMLog.LogVerbose($"PoolManager using custom destroy delegate for instance {instance.name}");
            Object.Destroy(instance);
        }

        #endregion
    }
}
#pragma warning restore 0649
