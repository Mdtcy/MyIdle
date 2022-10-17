/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-04-24 11:43:08
 * @modify date 2022-04-24 11:43:08
 * @desc [description]
 */

using System.IO;
using System.Text;
using HM;
using HM.Interface;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.Support.Archive
{
    public class ArchiveReceiver : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private GameObject goTip;

        // * injected
        private IArchive archive;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(IArchive archive)
        {
            this.archive = archive;
        }

        public void OnArchiveReceived(string filepath)
        {
            HMLog.LogInfo($"[ArchiveReceiver]收到存档:{filepath}");

            if (!archive.IsValidArchive(filepath))
            {
                HMLog.LogWarning($"[ArchiveReceiver]忽略非法存档:{filepath}");
                return;
            }

            // todo: 存档应定义后缀名

            archive.IsEnabled = false; // 禁止保存，免得覆盖存档
            // 将传入的存档内容覆盖到当前存档文件里

            var newContent = File.ReadAllText(filepath, Encoding.UTF8);
            var currPath   = archive.CurrentArchivePath;
            HMLog.LogInfo($"[ArchiveReceiver]当前存档路径:{currPath}");
            File.WriteAllText(currPath, newContent, Encoding.UTF8);

            // 删除该文件
            File.Delete(filepath);

            // 给出重进游戏的提示
            goTip.SetActive(true);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            goTip.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649