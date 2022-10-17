/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-02-19 11:02:51
 * @modify date 2021-02-19 11:02:51
 * @desc [对于只需要持久化而不需特殊处理的对象，可使用该类型完成持久化操作]
 */

using System;
using Sirenix.Utilities;
using Zenject;

namespace HM.Interface
{
    [Serializable]
    public class PersistableAdaptor : PersistableObject
    {
        // 需持久化对象
        private readonly object target;

        // 缓存一下存档key
        private readonly string persistKey;

        public PersistableAdaptor(object target)
        {
            this.target = target;
            persistKey = target.GetType().GetNiceName();
        }

        /// <inheritdoc />
        public override void OnWillInject(DiContainer container)
        {
            container.Inject(target);
        }

        /// <inheritdoc />
        public override void OnArchiveWillLoad(IArchive archive)
        {
            archive.LoadInto(PersistKey, target);
        }

        /// <inheritdoc />
        public override void OnArchiveWillSave(IArchive archive)
        {
            archive.Save(PersistKey, target);
        }

        /// <inheritdoc />
        public override string PersistKey => persistKey;
    }
}