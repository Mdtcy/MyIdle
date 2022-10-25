/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-27 12:07:38
 * @modify date 2020-07-27 12:07:38
 * @desc [在这里设置所有需要持久化存储的对象]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using DefaultNamespace.Age;
using HM;
using HM.Extensions;
using HM.GameBase;
using HM.Interface;
using UnityEngine;
using Zenject;

namespace NewLife.BusinessLogic.Archive
{
    public class ArchiveInitializer : MonoBehaviour
    {
        /// <summary>
        /// 持久化配置
        /// </summary>
        private struct PersistConfig
        {
            /// <summary>
            /// 需持久化的类型(会从Zenject里resolve)
            /// </summary>
            public readonly Type TypeToResolve;

            /// <summary>
            /// 可能会通过id识别具体类型
            /// </summary>
            public readonly string ZenjectId;

            public PersistConfig(Type type, string zenjectId)
            {
                TypeToResolve = type;
                ZenjectId     = zenjectId;
            }
        }

        #region FIELDS

        [Inject]
        private IArchive archive;

        [Inject]
        private DiContainer container;

        private readonly List<PersistConfig> configs = new List<PersistConfig>
        {
            new PersistConfig(typeof(Inventory), string.Empty),
            new PersistConfig(typeof(Age), string.Empty),
        };

        [Inject]
        private PersistInventory persistInventory;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 初始化需要持久化存储的对象
        /// </summary>
        public void Run()
        {
            HMLog.LogVerbose("[ArchiveInitializer]Run");

            // 使用persistInventory的用意是:
            // * 实现IPersistable接口的对象可以根据需要实现接口函数以完成不同阶段的自定义操作
            // * 统一加载存档、注入依赖后，再依次通知OnLoaded时，数据已经准备好了
            foreach (var config in configs)
            {
                object target = config.ZenjectId.IsNullOrEmpty()
                    ? container.Resolve(config.TypeToResolve)
                    : container.ResolveId(config.TypeToResolve, config.ZenjectId);

                if (target is IPersistable persistable)
                {
                    persistInventory.Add(persistable);
                }
                else
                {
                    persistInventory.Add(new PersistableAdaptor(target));
                }
            }

            archive.Register(persistInventory);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649