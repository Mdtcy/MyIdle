/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月25日
 * @modify date 2022年12月25日
 * @desc []
 */

#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IdleGame
{
    public class EntityUI : MonoBehaviour
    {
        #region FIELDS

        public Entity Entity;

        [SerializeField]
        private Text txtName;

        [SerializeField]
        private Text txtMaxHp;

        [SerializeField]
        private Text txtAtk;

        [SerializeField]
        private Slider hpBar;

        [SerializeField]
        private Text txtHp;

        [SerializeField]
        private Text txtCriticalProbability;

        [SerializeField]
        private Text txtCriticalDamage;

        [SerializeField]
        private Text txtDodgeProbability;

        [SerializeField]
        private Text txtAttackSpeed;

        [SerializeField]
        private Text txtFireInterval;

        [SerializeField]
        private Transform pfbText;

        [SerializeField]
        private Transform buffRoot;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            txtName.text = Entity.name;
            RefreshUI();
        }

        private void RefreshUI()
        {
            // txtMaxHp.text               = $"最大生命值 : {Entity.GetAttribute(AttributeType.MaxHp)}";
            // txtHp.text                  = $"当前生命值 : {Entity.GetResource(ResourceType.Hp)}";
            // hpBar.maxValue              = Entity.GetAttribute(AttributeType.MaxHp);
            // hpBar.value                 = Entity.GetResource(ResourceType.Hp);
            // txtAtk.text                 = $"攻击力 : {Entity.GetAttribute(AttributeType.Atk)}";
            // txtCriticalProbability.text = $"暴击概率 : {Entity.GetAttribute(AttributeType.CriticalChance)}";
            // txtCriticalDamage.text      = $"暴击伤害 : {Entity.GetAttribute(AttributeType.CriticalDamage)}";
            // txtDodgeProbability.text    = $"闪避概率 : {Entity.GetAttribute(AttributeType.DodgeChance)}";
            // txtAttackSpeed.text         = $"攻速 : {Entity.GetAttribute(AttributeType.AttackSpeed)}";
            //
            // float attackSpeed      = Entity.GetAttribute(AttributeType.AttackSpeed);
            // float baseFireInterval = Entity.GetAttribute(AttributeType.BaseFireInterval);
            // float fireInterval     = baseFireInterval / ((attackSpeed + 100) * 0.01f);

            // txtFireInterval.text        = $"攻击间隔 : {fireInterval}秒一次";


            for (int i = buffRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(buffRoot.GetChild(i).gameObject);
            }

            foreach (var buff in Entity.BuffComponent.Buffs)
            {
                Instantiate(pfbText, buffRoot).GetComponent<Text>().text = buff.model.name + " " +buff.stack;
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649