/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年11月6日
 * @modify date 2022年11月6日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Currency
{
    public class UICoin : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private TextMeshProUGUI txtCoin;

        [Inject]
        private ICurrencyOperator currencyOperator;

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
            RefreshUI();
            currencyOperator.ActOnCoinNumChanged += RefreshUI;
        }

        private void OnDisable()
        {
            currencyOperator.ActOnCoinNumChanged -= RefreshUI;
        }

        private void RefreshUI()
        {
            txtCoin.text = currencyOperator.CoinNum.ToString();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649