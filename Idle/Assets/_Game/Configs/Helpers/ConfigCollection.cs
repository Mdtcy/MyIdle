/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-02 22:05:07
 * @modify date 2020-05-02 22:05:07
 * @desc [description]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using HM.GameBase;
using Zenject;

namespace NewLife.Config.Helper
{
    public class ConfigCollection<T> : IHMPooledObject, IDisposable where T : BaseConfig
    {
        [Inject]
        private ConfigContainer configContainer;

        public void OnEnterPool()
        {
        }

        public void Dispose()
        {
            HM.GameBase.ListPool<T>.Release(configs);
            configs = null;
            var inst = this;
            ObjectPool<ConfigCollection<T>>.Release( ref inst);
        }

        public void Prepare(int capacity = 16)
        {
            if (configs == null)
            {
                configs = HM.GameBase.ListPool<T>.Claim(capacity);
            }
            else
            {
                configs.Clear();
            }

            configContainer.GetConfigs(ref configs);
            configs.Sort((a, b) => a.Id.CompareTo(b.Id));
        }

        public ListFastEnumerator<T> GetEnumerator()
        {
            return new ListFastEnumerator<T>(configs);
        }

        public List<T> ToList()
        {
            return configs;
        }

        public T this[int index] => configs[index];

        private List<T> configs;

        public int Count => configs.Count;
    }

    public class ConfigCollectionFactory
    {
        private readonly DiContainer container;

        public ConfigCollectionFactory(DiContainer container)
        {
            this.container = container;
        }

        public ConfigCollection<T> CreateConfigCollection<T>() where T : BaseConfig
        {
            var inst = ObjectPool<ConfigCollection<T>>.Claim();
            container.Inject(inst);
            inst.Prepare();

            return inst;
        }
    }
}
#pragma warning restore 0649