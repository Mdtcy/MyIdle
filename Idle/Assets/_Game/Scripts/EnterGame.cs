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
using NewLife.BusinessLogic.Archive;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnterGame : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private ArchiveInitializer archiveInitializer;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            archiveInitializer.Run();
            HMLog.LogDebug("加载存档");
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649