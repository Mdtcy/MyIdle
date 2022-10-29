/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-27 11:07:35
 * @modify date 2020-07-27 11:07:35
 * @desc [使用EasySave实现本地存储]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using System.IO;
using HM;
using HM.Interface;
using HM.Notification;
using NewLife.Defined;
using UnityEngine;
using Zenject;

namespace NewLife.BusinessLogic.Archive
{
    public class EasySaveArchive : IArchive
    {
        #region FIELDS

        // 存档加载到ES3File缓存，提高访问性能
        private ES3File file;

        // 所有需要持久化保存的对象都要注册到这里
        private readonly HashSet<IArchiveClient> archivedItems = new HashSet<IArchiveClient>();

        // 保存所有存档名的文件
        private const string KeyFileName = "archiveKeys.es3";

        // 最近一次使用的存档名
        private const string KCurrentArchive = "KCurrentArchive";

        // inject
        private ISendNotification       notificationSender;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 当前使用的存档名
        /// </summary>
        private string CurrentArchiveName { get; set; }

        public string ArchiveName => CurrentArchiveName;

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(ISendNotification       notificationSender)
        {
            this.notificationSender = notificationSender;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnArchiveCreated()
        {
            file.Save("ArchiveId", Application.identifier);
            file.Save("Version", "1.0");
        }

        #endregion

        #region STATIC METHODS

        #endregion

        #region IArchive

        /// <inheritdoc />
        public bool ArchiveExists(string name)
        {
            bool keyExists  = ES3.KeyExists(name, KeyFileName);
            bool fileExists = ES3.FileExists(name);

            HMLog.LogDebug($"[IArchive] ArchiveExists = [name = {name}, keyExists = {keyExists}, fileExists = {fileExists}]");

            return keyExists && fileExists;
        }

        /// <inheritdoc />
        public void CreateAndLoad(string name)
        {
            Debug.Assert(!ArchiveExists(name));

            // 保存新的存档名到key file
            ES3.Save(name, true, KeyFileName);

            // 更新当前存档名
            ES3.Save(KCurrentArchive, name, KeyFileName);
            CurrentArchiveName = name;

            // 加载
            Load(CurrentArchiveName);
            OnArchiveCreated();
        }

        /// <inheritdoc />
        public void Register(IArchiveClient itemToArchive)
        {
            archivedItems.Add(itemToArchive);
        }

        /// <inheritdoc />
        public void Save()
        {
            if (!IsEnabled)
            {
                HMLog.LogWarning($"[EasySaveArchive] is disabled, cannot save.");

                return;
            }

            foreach (var item in archivedItems)
            {
                item.OnArchiveWillSave(this);
            }

            file.Sync();
        }

        /// <inheritdoc />
        public void Load(string name)
        {
            IsEnabled          = true; // 默认可保存
            CurrentArchiveName = name;
            file               = new ES3File(CurrentArchiveName);
            HMLog.LogVerbose($"[EasySaveArchive] Load {CurrentArchiveName}");

            foreach (var item in archivedItems)
            {
                item.OnArchiveWillLoad(this);
            }
        }

        /// <inheritdoc />
        public bool Exists()
        {
            if (!ES3.FileExists(KeyFileName))
            {
                return false;
            }

            string currentArchiveName = ES3.Load<string>(KCurrentArchive, KeyFileName);

            if (string.IsNullOrEmpty(currentArchiveName))
            {
                return false;
            }

            return ArchiveExists(currentArchiveName);
        }

        /// <inheritdoc />
        public void LoadInto<T>(string key, T target) where T : class
        {
            if (file.KeyExists(key))
            {
                file.LoadInto(key, target);
            }
            else
            {
                HMLog.LogWarning($"[EasySaveArchive]Error trying to load into {typeof(T)} with key={key}: key not exists!");
            }
        }

        /// <inheritdoc />
        public void Save<T>(string key, T value)
        {
            file.Save(key, value);
        }

        /// <inheritdoc />
        public void Backup(string name)
        {
            HMLog.LogVerbose($"[EasySaveArchive]备份当前存档到:backup_{name}");
            ES3.CopyFile(CurrentArchiveName, $"backup_{name}");
        }

        /// <inheritdoc />
        public bool IsEnabled { get; set; }

        /// <inheritdoc />
        public string CurrentArchivePath => file.settings.FullPath;

        /// <inheritdoc />
        public ES3File CurrentFile => file;

        /// <inheritdoc />
        public bool IsValidArchive(string filepath)
        {
            try
            {
                var content = File.ReadAllText(filepath);

                if (content.Contains(Application.identifier)) // 暂时认为包含该字符串则为有效存档
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                HMLog.LogError($"ERROR trying to validate archive: {filepath}, exception:{e}");

                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public void ReplaceArchive(string newArchivePath, string name)
        {
            // 如果本地没有这个人的存档，创建一份
            if (!ArchiveExists(name))
            {
                // 保存新的存档名到key file
                ES3.Save(name, true, KeyFileName);
            }

            ES3.CopyFile(newArchivePath, name);
        }

        /// <inheritdoc />
        public string Export()
        {
            return ES3.LoadRawString(CurrentArchiveName);
        }

        #endregion
    }
}
#pragma warning restore 0649