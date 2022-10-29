/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-27 11:07:51
 * @modify date 2020-07-27 11:07:51
 * @desc [使用持久化存储的请求]
 */

using HM;
using HM.GameBase;
using HM.Interface;
using HM.Notification;

namespace NewLife.BusinessLogic.Request
{
    public class RequestLocal : RequestMem
    {
        #region FIELDS

        private readonly IArchive            archive;
        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS

        /// <inheritdoc />
        public override void FastLogin(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogVerbose("Request::FastLogin");
            if (archive.ArchiveExists(name))
            {
                Login(name, onSucc, onFail);
            }
            else
            {
                Register(name, onSucc, onFail);
            }
        }

        /// <inheritdoc />
        public override void Register(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogVerbose("Request::Register");
            archive.CreateAndLoad(name);
            onSucc?.Invoke();
        }

        /// <inheritdoc />
        public override void Login(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogVerbose("Request::Login");
            archive.Load(name);
            onSucc?.Invoke();
        }

        /// <inheritdoc />
        public override void Save(OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogVerbose("Request::Save");
            archive.Save();
            onSucc?.Invoke();
        }

        public override void Backup(string name, OnRequestSucc onSucc = null, OnRequestFail onFail = null)
        {
            HMLog.LogInfo($"Request::Backup {name}");
            Save();
            archive.Backup(name);
            onSucc?.Invoke();
        }

        #endregion

        #region PROTECTED METHODS

        /// <inheritdoc />
        protected RequestLocal(Inventory inventory, IItemFactory factory, IArchive archive, ISendNotification sendNotification) : base(inventory, factory, sendNotification)
        {
            this.archive             = archive;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
