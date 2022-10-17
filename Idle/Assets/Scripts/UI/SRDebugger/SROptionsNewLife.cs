/**
 * @author [Boluo]
 * @email [tktetb@163.com]
 * @create date 2020-1-5
 * @modify date 2020-1-5
 * @desc [SROption 匿名爱人]
 */

#pragma warning disable 0649

using System.ComponentModel;
using System.IO;
using HM;
using HM.GameBase;
using HM.Interface;
using NewLife.BusinessLogic.Archive;
using NewLife.Defined;
using UnityEngine;
using Zenject;

namespace NewLife.UI.SRDebuggers
{
    /// <summary>
    /// SROption 匿名爱人
    /// </summary>
    public class SROptionsNewLife : SROptions
    {
        #region FIELDS

        [Inject]
        private IItemUpdater itemUpdater;

        [Inject]
        private EasySaveArchive easySaveArchive;

        #endregion

        #region PROPERTIES

        [Category("Game")]
        [DisplayName("10倍速")]
        public void TimeScaleX10()
        {
            Time.timeScale = 10;
        }

        [Category("Game")]
        [DisplayName("恢复一倍速")]
        public void ResetTimeScale()
        {
            Time.timeScale = 1;
        }

        [Category("Game")]
        [DisplayName("分享存档")]
        public void ShareArchive()
        {
            var archivePath = Path.Combine(Application.persistentDataPath, easySaveArchive.ArchiveName);
            HMLog.LogVerbose($"archive path = {archivePath}");
            HMLog.LogVerbose($"archive exists = {File.Exists(archivePath)}");
            #if UNITY_EDITOR
            HMLog.LogVerbose("不支持分享到该平台");
            #elif UNITY_ANDROID || UNITY_IPHONE
            new NativeShare().AddFile(archivePath).Share();
            #else
            HMLog.LogVerbose("不支持分享到该平台");
            #endif
        }

        [Category("Inventory")]
        [DisplayName("物品Id")]
        public int ItemId { get; set; }

        [Category("Inventory")]
        [DisplayName("物品数量")]
        public int ItemNum { get; set; }

        #endregion

        #region PUBLIC METHODS

        [Category("Inventory")]
        [DisplayName("增加物品")]
        public void AcquireItem()
        {
            itemUpdater.AddItem(ItemId, ItemNum);
        }

        [Category("Inventory")]
        [DisplayName("消耗物品")]
        public void ConsumeItem()
        {
            itemUpdater.ConsumeItem(ItemId, ItemNum);
        }


        #region 属性
        #endregion

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC FIELDS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649