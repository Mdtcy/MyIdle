/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年11月6日
 * @modify date 2022年11月6日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System;
using Game.PlayerAge;
using HM.GameBase;
using NewLife.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Profession
{
    public class UIProfession : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private ProfessionConfig professionConfig;

        [SerializeField]
        private TextMeshProUGUI txtName;

        [SerializeField]
        private TextMeshProUGUI txtExp;

        [SerializeField]
        private TextMeshProUGUI txtLevel;

        [SerializeField]
        private Slider expSlider;

        // local
        private ItemProfession itemProfession;

        // inject
        [Inject]
        private IItemUpdater itemUpdater;

        #endregion

        #region PROPERTIES

        private ItemProfession ItemProfession
        {
            get
            {
                itemProfession ??= itemUpdater.GetOrAddItem<ItemProfession>(professionConfig.Id);

                return itemProfession;
            }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnEnable()
        {
            txtName.text                   =  professionConfig.Name;
            RefreshUI();
            ItemProfession.ActOnExpChanged += RefreshUI;
        }

        private void OnDisable()
        {
            ItemProfession.ActOnExpChanged -= RefreshUI;
        }

        private void RefreshUI()
        {
            txtExp.text        = ItemProfession.Exp.ToString();
            txtLevel.text      = ItemProfession.Level.ToString();
            expSlider.maxValue = ItemProfession.TotalExpToNextLevel();
            expSlider.value    = ItemProfession.Exp;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649