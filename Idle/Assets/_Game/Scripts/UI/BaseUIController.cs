/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月20日
 * @modify date 2022年10月20日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UnityEngine;
using Zenject;

namespace _Game.Scripts.UI
{
    public class BaseUIController<T>  where T : MonoBehaviour
    {
        #region FIELDS

        protected T View;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public BaseUIController(T tView)
        {
            View = tView;
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