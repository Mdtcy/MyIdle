/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date 9:58:05
 * @modify date 9:58:05
 * @desc [开始游戏的日期]
 */

#pragma warning disable 0649
using HM;
using HM.Date;
using Zenject;

namespace NewLife.BusinessLogic.DateTimeUtils
{
    /// <summary>
    /// 开始游戏的日期
    /// </summary>
    public class StartDate
    {
        #region FIELDS

        // local
        private IDateTime dateTime;

        // 开始游戏时的日期
        [ES3Serializable]
        private System.DateTime startDateTime;

        // 是否初始化
        [ES3Serializable]
        private bool hasInit;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(IDateTime dateTime)
        {
            this.dateTime   = dateTime;
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <returns></returns>
        public System.DateTime Get()
        {
            HMLog.Assert(hasInit, "[StartDate] StartDate未初始化。");
            return startDateTime;
        }

        /// <summary>
        /// 如果还没初始化则进行初始化
        /// </summary>
        public void InitializeIfNecessary()
        {
            if (!hasInit)
            {
                Init();
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        // 初始化开始游戏日期
        private void Init()
        {
            hasInit       = true;
            startDateTime = dateTime.Now();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}

#pragma warning restore 0649