/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-11 16:06:10
 * @modify date 2020-06-11 16:06:10
 * @desc [跟随GameObject生命周期，对事件进行监听]
 */

namespace NewLife.Support.Notification
{
    public class LifetimeNotificationAgent : NotificationAgent
    {
        #region FIELDS
        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS
        #endregion

        #region PROTECTED METHODS

        /// <inheritdoc />
        protected override void OnDisable()
        {
            // will skip onEnable/OnDisable pause/resume
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            // will skip onEnable/OnDisable pause/resume
        }

        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion
    }
}