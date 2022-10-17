/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-02-22 17:53:30
 * @modify date 2022-02-22 17:53:30
 * @desc [description]
 */

using HM;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.SpawnPoolExtension
{
    public class SpawnPoolInjector : MonoBehaviour
    {
        #region FIELDS

        // 需配合SpawnPool使用，spawnPool从此处借用/归还预加载的gameObject
        [SerializeField]
        private SpawnPool pool;

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

        [Button("一键填充SpawnPool")]
        private void Setup()
        {
            pool = GetComponent<SpawnPool>();

            if (!pool)
            {
                HMLog.LogWarning($"在当前GameObject上未找到SpawnPool!");
            }
        }

        private void Awake()
        {
            HMLog.LogVerbose($"[SpawnPoolInjector[{name}]] 向SpawnPool[{pool.name}]注册");
            pool.instantiateDelegates = InstantiateDelegates;
            pool.destroyDelegates     = DestroyDelegates;
        }

        // 归还不再使用的对象实例
        private void DestroyDelegates(GameObject instance)
        {

            HMLog.LogVerbose($"[SpawnPoolInjector[{name}]] destroy instance[{instance.name}] directly.");
            Destroy(instance);
        }

        // 借用指定prefab的实例
        private GameObject InstantiateDelegates(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            HMLog.LogVerbose($"[SpawnPoolInjector[{name}]] instantiate prefab[{prefab.name}].");

            var newGo = instantiator != null ? instantiator.InstantiatePrefab(prefab) : Instantiate(prefab);
            newGo.transform.localPosition = pos;
            newGo.transform.rotation = rot;
            newGo.SetActive(true);
            return newGo;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649