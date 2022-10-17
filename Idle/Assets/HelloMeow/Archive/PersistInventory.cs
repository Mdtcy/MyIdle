/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-02-05 21:02:05
 * @modify date 2021-02-05 21:02:05
 * @desc [用来统一管理持久化对象的初始化顺序]
 */

using System;
using System.Collections.Generic;
using Zenject;

namespace HM.Interface
{
    [Serializable]
    public class PersistInventory : IArchiveClient
    {
        // 需持久化的对象
        [UnityEngine.SerializeField]
        private Dictionary<string, IPersistable> items = new Dictionary<string, IPersistable>();

        // * injected
        private readonly DiContainer container;

        [Inject]
        public PersistInventory(DiContainer container)
        {
            this.container = container;
        }

        public PersistInventory()
        {}

        /// <summary>
        /// 增加可持久化对象
        /// </summary>
        /// <param name="target"></param>
        public void Add(IPersistable target)
        {
            HMLog.LogVerbose($"[PersistInventory]Add {target.PersistKey}");
            items.Add(target.PersistKey, target);
        }

        #region IArchiveClient

        /// <inheritdoc />
        public void OnArchiveWillLoad(IArchive archive)
        {
            HMLog.LogVerbose("[PersistInventory]Load");

            foreach (var client in items.Values)
            {
                HMLog.LogVerbose($"[PersistInventory]Load {client.PersistKey}");
                client.OnArchiveWillLoad(archive);
            }

            foreach (var client in items.Values)
            {
                client.OnWillInject(container);
            }

            foreach (var client in items.Values)
            {
                client.OnLoaded();
            }
        }

        /// <inheritdoc />
        public void OnArchiveWillSave(IArchive archive)
        {
            HMLog.LogVerbose("[PersistInventory]Save");

            foreach (var client in items.Values)
            {
                client.OnWillSave();
            }

            foreach (var client in items.Values)
            {
                client.OnArchiveWillSave(archive);
            }
        }

        #endregion
    }
}