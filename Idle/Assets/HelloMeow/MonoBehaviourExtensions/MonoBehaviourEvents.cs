/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-04-28 09:04:48
 * @modify date 2021-04-28 09:04:48
 * @desc [方便在Inspector里指定Unity事件回调]
 */

using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0649
namespace HM.MonoBehaviourExtensions
{
    public class MonoBehaviourEvents : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private UnityEvent onStart;

        [SerializeField]
        private UnityEvent onAwake;

        [SerializeField]
        private UnityEvent onEnable;

        [SerializeField]
        private UnityEvent onDisable;

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
            onAwake?.Invoke();
        }

        private void Start()
        {
            onStart?.Invoke();
        }

        private void OnEnable()
        {
            onEnable?.Invoke();
        }

        private void OnDisable()
        {
            onDisable?.Invoke();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649