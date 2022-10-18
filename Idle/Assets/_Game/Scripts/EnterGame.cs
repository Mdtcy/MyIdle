/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月18日
 * @modify date 2022年10月18日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using HM;
using HM.Interface;
using NewLife.BusinessLogic.Archive;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class EnterGame : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private ArchiveInitializer archiveInitializer;

        [Inject]
        private IRequest request;

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
            archiveInitializer.Run();
        }

        // 登录成功
        private void OnUserLogin()
        {
            HMLog.LogInfo($"LoginOnGameScene::登陆成功:");
        }
        private void Start()
        {
            request.FastLogin("IdleArchive", OnUserLogin);
            HMLog.LogDebug("加载存档");
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649