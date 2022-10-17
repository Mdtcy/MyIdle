/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-22 10:54:15
 * @modify date 2018-12-22 10:54:15
 * @desc [配置管理器]
 */

using System;
using System.Collections.Generic;
using HM.DataStructures;
using HM.Extensions;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
#endif

namespace HM.GameBase
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    [ExecuteInEditMode]
    public class ConfigContainer : MonoBehaviour
    {
        /// <summary>
        /// 保存运行时已经加载的配置
        /// </summary>
        [Serializable]
        private class BaseConfigMap : SerializableDictionary<int, BaseConfig>
        {
        }
        #region FIELDS

        [SerializeField]
        private OneToListDictionary<Type, BaseConfig> typeGroupedConfigs =
            new OneToListDictionary<Type, BaseConfig>();

        // 保存所有已加载的配置
        [SerializeField]
        private BaseConfigMap configMap = new BaseConfigMap();

        // 需要初始化typeGroupedConfigs
        private bool hasInitialized;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 根据itemId查询并返回相应的物品配置
        /// </summary>
        /// <param name="id">物品的itemId</param>
        /// <typeparam name="T">物品类型</typeparam>
        /// <returns></returns>
        public T GetConfig<T>(int id) where T : BaseConfig
        {
            return GetBaseConfig(id) as T;
        }

        // 根据itemId查询并返回相应的物品Base配置
        private BaseConfig GetBaseConfig(int id)
        {
            return configMap.GetValue(id, null);
        }

        /// <summary>
        /// 查询并返回某个类型的全部配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public void GetConfigs<T>(ref List<T> list)
            where T : BaseConfig
        {
            if (!hasInitialized)
            {
                Initialize();
            }

            foreach (var pair in typeGroupedConfigs)
            {
                var type = pair.Key;
                if (type == typeof(T) || type.IsSubclassOf(typeof(T)))
                {
                    foreach (var config in pair.Value)
                    {
                        list.Add(config as T);
                    }
                }
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            if (!hasInitialized)
            {
                Initialize();
            }
        }

        // 运行时再初始化TypeGroupedConfig，序列化有点问题
        private void Initialize()
        {
            HMLog.LogVerbose("[ConfigContainer]将配置按类型分组存放");
            hasInitialized = true;

            typeGroupedConfigs.Clear();
            foreach (var pair in configMap)
            {
                var config = pair.Value;
                typeGroupedConfigs.Add(config.GetType(), config);
            }
        }

#if UNITY_EDITOR
        [BoxGroup("编辑器模式下可用")]
        [LabelText("显示详细日志")]
        [SerializeField]
        private bool showDetail;

        // 在编辑状态生成配置字典，没有必要运行时加载，这样可以提高效率
        [BoxGroup("编辑器模式下可用")]
        [Button("重新加载所有配置")]
        public void Reload()
        {
            //此处如果使用Resources.LoadAll("",typeof(BaseConfig))在某些机型上会莫名报错，因此改为按文件夹分别读取的方式
            Debug.Log("[ConfigContainer]重新加载所有配置");
            configMap.Clear();

            foreach (string guid in AssetDatabase.FindAssets("t:BaseConfig"))
            {
                var config = AssetDatabase.LoadAssetAtPath<BaseConfig>(AssetDatabase.GUIDToAssetPath(guid));
                configMap.Add(config.Id, config);

                if (showDetail)
                {
                    HMLog.LogVerbose($"==> Load Config:{config.name}");
                }
            }

            HMLog.LogVerbose($"[ConfigContainer]加载完毕，共加载{configMap.Count}个配置");
        }
#endif

        #endregion

        #region STATIC METHODS

        #endregion
    }
}