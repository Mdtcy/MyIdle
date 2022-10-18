/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-27 20:07:27
 * @modify date 2020-07-27 20:07:27
 * @desc [存档适配器]
 */

using HM.Interface;
using UnityEngine;

namespace NewLife.BusinessLogic.Archive
{
    public class ArchiveAdaptor : IArchiveClient
    {
        // 被适配的对象
        [ES3Serializable]
        private readonly object item;

        // 存档key
        private readonly string key;

        /// <inheritdoc />
        public ArchiveAdaptor(string key, object item)
        {
            Debug.Assert(item != null);
            this.item = item;
            this.key = key;
        }

        #region IArchiveClientt

        /// <inheritdoc />
        public void OnArchiveWillLoad(IArchive archive)
        {
            archive.LoadInto(key, item);
        }

        /// <inheritdoc />
        public void OnArchiveWillSave(IArchive archive)
        {
            archive.Save(key, item);
        }

        #endregion
    }
}
