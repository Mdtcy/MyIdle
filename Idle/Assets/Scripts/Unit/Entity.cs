/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UnityEngine;
using Zenject;

namespace Test
{
    public class Entity : MonoBehaviour
    {
        #region FIELDS

        public string name;

        public int    attack;

        public float maxHp;

        public float hp;

        public float criticalProbability;

        public float criticalDamageRatio;

        public float dodgeProbability;


        public BuffComponent BuffComponent;


        public float testC;
        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Attack(Entity other)
        {
            // 闪避判断
            if (Random.Range(0f, 1f) <= dodgeProbability)
            {
                other.OnMiss();
            }
            // 命中敌人
            else
            {
                float damage = attack;

                float c = criticalProbability;

                foreach (var buff in BuffComponent.Buffs)
                {
                    if (buff is AddCriticalProbability addCriticalProbability)
                    {
                        c += addCriticalProbability.CriticalProbabilityAdd;
                    }
                }

                testC = c;
                if (Random.Range(0f, 1f) <= c)
                {
                    damage *= criticalDamageRatio;
                }

                other.OnHurt(damage);
            }
        }

        public void OnHurt(float damage)
        {
            if (damage >= hp)
            {
                OnDeath();
            }
            else
            {
                hp -= damage;
                signalBus.Fire(new OnHpUpdatedSignal(this));
            }

        }
        [Inject]
        private SignalBus signalBus;

        public void OnDeath()
        {
            Destroy(gameObject);
        }

        public void OnMiss()
        {
            Debug.Log($"{name} Miss");
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