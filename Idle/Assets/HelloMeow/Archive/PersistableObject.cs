/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-02-06 11:02:43
 * @modify date 2021-02-06 11:02:43
 * @desc [可持久化对象，可继承该类型以自动获取持久化能力]
 */

using System;
using Sirenix.Utilities;
using Zenject;

namespace HM.Interface
{
    [Serializable]
    public class PersistableObject : IPersistable, IInitializable
    {
        // 存档key
        private readonly string persistKey;

        public PersistableObject()
        {
            // 缓存一下存档key
            persistKey = GetType().GetNiceName();
        }

        /// <inheritdoc />
        public virtual void OnWillInject(DiContainer container)
        {
        }

        /// <inheritdoc />
        public virtual void OnLoaded()
        {
        }

        /// <inheritdoc />
        public virtual void OnWillSave()
        {
        }

        /// <inheritdoc />
        [UnityEngine.SerializeField]
        public virtual string PersistKey => persistKey;

        /// <inheritdoc />
        public virtual void OnArchiveWillLoad(IArchive archive)
        {
            archive.LoadInto(PersistKey, this);
        }

        /// <inheritdoc />
        public virtual void OnArchiveWillSave(IArchive archive)
        {
            archive.Save(PersistKey, this);
        }

        public virtual void Initialize()
        {
        }
    }
}