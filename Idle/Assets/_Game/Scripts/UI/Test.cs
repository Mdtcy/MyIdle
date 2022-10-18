/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月18日
 * @modify date 2022年10月18日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using HM;
using HM.Interface;
using NewLife.Config;
using NewLife.Config.Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.UI
{
    public class Test : MonoBehaviour
    {
        #region FIELDS

        // [Inject]
        // private IConfigGetter configGetter;

        [Inject]
        private ConfigCollectionFactory configCollectionFactory;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Button]
        public void 测试()
        {
            using (var col = configCollectionFactory.CreateConfigCollection<TestConfig>())
            {
                foreach (var test in col)
                {
                    HMLog.LogDebug($"var {test}");
                }
            }
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649