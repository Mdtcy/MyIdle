/**
 * @author [Boluo]
 * @email [tktetb@163.com]
 * @create date 2022年10月14日 10:35:08
 * @modify date 2022年10月14日 10:35:08
 * @desc [点击音效开关 默认开启 不保存状态]
 */

#pragma warning disable 0649
namespace NewLife.BusinessLogic.Effect
{
    /// <summary>
    /// 点击音效开关 默认开启 不保存状态
    /// </summary>
    public class ClickEffectSwitch
    {
        #region FIELDS

        // 是否开启
        private bool isOn = true;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsOn
        {
            get => isOn;
            set => isOn = value;
        }

        #endregion

        #region PUBLIC METHODS

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