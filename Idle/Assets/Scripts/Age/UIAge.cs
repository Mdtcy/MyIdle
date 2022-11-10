/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月26日
 * @modify date 2022年10月26日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.PlayerAge
{
    public class UIAge : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private TextMeshProUGUI txtAge;

        [Inject]
        private Age age;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnEnable()
        {
            age.ActOnAgeDayChange += OnAgeChanged;
        }

        private void OnDisable()
        {
            age.ActOnAgeDayChange -= OnAgeChanged;
        }

        private void OnAgeChanged()
        {
            txtAge.text = age.ToString();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649