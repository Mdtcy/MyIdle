/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.ComponentModel;
using Event;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Test
{
    public class CombatTest : MonoBehaviour
    {
        #region FIELDS

        [InlineEditor(InlineEditorModes.FullEditor)]
        public Entity Entity1;

        [InlineEditor(InlineEditorModes.FullEditor)]
        public Entity Entity2;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Button]
        public void Entity1Attack2()
        {
            Entity1.Attack(Entity2);
        }

        [Button]
        public void Entity2Attack1()
        {
            Entity2.Attack(Entity1);
        }

        [Inject]
        private DiContainer diContainer;

        [Button]
        public void AddAddCriticalProbabilityWhenHpLower30ToEntity1()
        {
            var buff = new AddCriticalProbabilityWhenHpLower30Percent(Entity1)
            {
                Permanent = true,
                Duration = 100
            };

            diContainer.Inject(buff);
            Entity1.BuffComponent.AddBuff(buff);
        }

        [Button]
        public void EventHpHigher30Percent()
        {
            Entity1.BuffComponent.TriggerEvent(new EEventHpHigher30Percent());
        }

        private SignalBus signalBus;

        [Button]
        public void EventHpLower30Percent()
        {
            Entity1.BuffComponent.TriggerEvent(new EEventHpLower30Percent());
        }

        [Button]
        public void ChangeEntity1Hp(int changed)
        {
            Entity1.ChangedHp(changed);
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