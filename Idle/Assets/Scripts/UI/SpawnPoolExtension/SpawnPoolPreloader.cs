/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-11-22 20:11:54
 * @modify date 2020-11-22 20:11:54
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using HM;
using HM.Extensions;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
#if UNITY_EDITOR
using UnityEditor;
#endif

#pragma warning disable 0649
namespace NewLife.SpawnPoolExtension
{
    /// <summary>
    /// 建议和SpawnPool绑定在同一个GameObject上
    /// * 需设置脚本执行顺序早于Default Time
    /// * SpawnPool不需要设置prefabPool，也不需要设置pool.instantiateDelegates
    /// </summary>
    public class SpawnPoolPreloader : MonoBehaviour
    {
        /// <summary>
        /// 预加载设置
        /// </summary>
        [Serializable]
        public class PreloadSetting
        {
            /// <summary>
            /// 需要预加载的prefab
            /// </summary>
            public GameObject Prefab;

            /// <summary>
            /// 需提前加载的数量
            /// </summary>
            [MinValue(1)]
            public int Amount = 1;

            /// <summary>
            /// 已加载的gameObject列表
            /// </summary>
            [HideInInspector]
            public List<GameObject> Preloaded = new List<GameObject>();

            /// <summary>
            /// 已使用、尚未回收的gameObject列表
            /// </summary>
            [HideInInspector]
            public List<GameObject> Spawned = new List<GameObject>();
        }

        #region FIELDS

        // 预加载prefab设置列表
        [TableList]
        [SerializeField]
        private List<PreloadSetting> settings;

        // 需配合SpawnPool使用，spawnPool从此处借用/归还预加载的gameObject
        [SerializeField]
        private SpawnPool pool;

        // 预加载的gameObject挂载处
        [SerializeField]
        private Transform PreloadedParent;

        [Inject]
        private IInstantiator instantiator;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            HMLog.LogVerbose($"[SpawnPoolPreloader[{name}]] 向SpawnPool[{pool.name}]注册");
            pool.instantiateDelegates = InstantiateDelegates;
            pool.destroyDelegates     = DestroyDelegates;
        }

        // 归还不再使用的对象实例
        private void DestroyDelegates(GameObject instance)
        {
            foreach (var setting in settings)
            {
                if (setting.Spawned.Contains(instance))
                {
                    setting.Spawned.Remove(instance);
                    setting.Preloaded.Add(instance);
                    HMLog.LogVerbose($"[SpawnPoolPreloader[{name}]] Despawn {instance.name} from SpawnPool, now remain count = {setting.Preloaded.Count}");

                    return;
                }
            }

            HMLog.LogVerbose($"[SpawnPoolPreloader[{name}]] Failed to find {instance.name}, destroy directly.");
            Destroy(instance);
        }

        // 借用指定prefab的实例
        private GameObject InstantiateDelegates(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            foreach (var setting in settings)
            {
                if (setting.Prefab == prefab && !setting.Preloaded.IsNullOrEmpty())
                {
                    var inst = setting.Preloaded.LastOne();
                    inst.transform.localPosition = pos;
                    inst.transform.rotation = rot;
                    setting.Spawned.Add(inst);
                    setting.Preloaded.RemoveLast();

                    HMLog.LogVerbose($"[SpawnPoolPreloader[{name}]] - Spawn preloaded prefab = {prefab.name}, now remain count = {setting.Preloaded.Count}");

                    return inst;
                }
            }

            HMLog.LogVerbose($"[SpawnPoolPreloader[{name}]] no (extra) preloaded prefab = {prefab.name}, instantiate directly.");

            var newGo = instantiator.InstantiatePrefab(prefab);
            newGo.transform.localPosition = pos;
            newGo.transform.rotation = rot;
            return newGo;
        }

        #if UNITY_EDITOR
        [Button(ButtonSizes.Medium)]
        private void Preload()
        {
            foreach (var setting in settings)
            {
                if (setting.Prefab == null)
                {
                    HMLog.LogVerbose("Prefab is null, cannot preload.");

                    continue;
                }

                PreloadedParent.DestroyChildrenImmediateWithName(setting.Prefab.name);
                setting.Preloaded.Clear();

                for (int i = 0; i < setting.Amount; i++)
                {
                    var go = PrefabUtility.InstantiatePrefab(setting.Prefab, PreloadedParent) as GameObject;
                    go.name = setting.Prefab.name;
                    go.SetActive(false);
                    setting.Preloaded.Add(go);
                }

                HMLog.LogVerbose($"Preload {setting.Amount} prefab(s) [name = {setting.Prefab.name}]");
            }
        }

        [Button(ButtonSizes.Medium)]
        private void ClearAll()
        {
            PreloadedParent.DestroyChildrenImmediate();
        }
        #endif

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649