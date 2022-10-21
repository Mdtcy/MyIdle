/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月18日
 * @modify date 2022年10月18日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using _Game.Scripts.UI.MainMenu;
using HM;
using HM.Interface;
using NewLife.BusinessLogic.Archive;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameEntry : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private ArchiveInitializer archiveInitializer;

        [Inject]
        private IRequest request;

        [Inject]
        private MainMenuController mainMenuController;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            HMLog.LogInfo($"[GameEntry] 加载存档");
            archiveInitializer.Run();
        }

        private void Start()
        {
            HMLog.LogInfo($"[GameEntry] 登陆游戏");
            request.FastLogin("IdleArchive", OnUserLogin);
        }

        // 登录成功
        private void OnUserLogin()
        {
            HMLog.LogInfo($"[GameEntry] 登陆成功");
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649