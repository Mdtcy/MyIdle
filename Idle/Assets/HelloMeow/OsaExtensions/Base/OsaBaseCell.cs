/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-11-10 15:11:48
 * @modify date 2021-11-10 15:11:48
 * @desc [每个cell的基类，是可选项，如果需要动态更新cell的size，那么继承自该脚本可以方便实现该功能]
 */

using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HM.OsaExtensions
{
    public class OsaBaseCell : MonoBehaviour
    {
        #region FIELDS

        [Inject]
        private SignalBus signalBus;

        [SerializeField]
        [ReadOnly]
        private int itemIndex;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 当前cell的索引(自动设置)
        /// </summary>
        public int ItemIndex
        {
            get => itemIndex;
            set => itemIndex = value;
        }

        public OsaBaseViewsHolder ViewsHolder { get; set; }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        // 更新当前cell的size到指定值
        protected void RequestSizeChange(float newSize)
        {
            if (signalBus.IsSignalDeclared<OsaRequestSizeChangeSignal>())
            {
                signalBus.Fire(new OsaRequestSizeChangeSignal(ViewsHolder.ItemIndex, newSize));
            }
            else
            {
                HMLog.LogWarning("未声明OsaRequestSizeChangeSignal，无法实现更新size的请求");
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}