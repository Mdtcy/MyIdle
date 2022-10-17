/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-08-09 18:08:14
 * @modify date 2021-08-09 18:08:14
 * @desc [可序列化的配置]
 */

using System;
using HM;
using HM.GameBase;
using HM.Interface;
using UnityEngine;
using Zenject;

namespace NewLife.Config
{
    [Serializable]
    public class SerializableConfig<T> where T : BaseConfig
    {
        #region FIELDS

        // 配置id
        [SerializeField]
        private int configId;

        private T config;

        // * injected
        [Inject]
        private IConfigGetter configGetter;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 获取配置
        /// </summary>
        public T Value
        {
            get
            {
                if (configId == 0)
                {
                    return null;
                }

                if (configGetter == null)
                {
                    HMLog.LogDebug($"varconfigGetter == null ");
                }

                return config ? config : config = configGetter.GetConfig<T>(configId);
            }
            set
            {
                config = value;
                configId = config ? config.Id : 0;
            }
        }

        /// <summary>
        /// 配置Id
        /// </summary>
        public int Id
        {
            get => Value != null ? Value.Id : 0;
            set
            {
                configId = value;
                config   = null; // 需重新获取
            }
        }

        #endregion

        #region PUBLIC METHODS

        public SerializableConfig()
        {}


        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }


    public class SerializableConfigFactory
    {
        private readonly DiContainer container;

        public SerializableConfigFactory(DiContainer container)
        {
            this.container = container;
        }

        public SerializableConfig<T> Create<T>() where T : BaseConfig
        {
            var inst = new SerializableConfig<T>();
            container.Inject(inst);
            return inst;
        }

        public SerializableConfig<T> Create<T>(T config) where T : BaseConfig
        {
            var inst = new SerializableConfig<T>();
            container.Inject(inst);
            inst.Value = config;
            return inst;
        }
    }
}