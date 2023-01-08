/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleGame
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


        // [Button]
        // public void AddAddCriticalProbabilityWhenHpLower30ToEntity1()
        // {
        //     var buff = new AddBuffInfo(new Add)
        //
        //     // diContainer.Inject(buff);
        //     Entity1.BuffComponent.AddBuff(buff, null, Entity1.gameObject ,1, true);
        // }

        // [Button]
        // public void AddAdd100AtkSpeedBuff()
        // {
        //     var buff = new Add100AtkSpeedBuff(Entity1);
        //
        //     // diContainer.Inject(buff);
        //     Entity1.BuffComponent.AddBuff(buff, null, Entity1.gameObject, 1 , true);
        // }
        //
        // [Button]
        // public void AddAddPoint1CriticalProb()
        // {
        //     var buff = new AddPoint1CriticalProb(Entity1);
        //
        //     // diContainer.Inject(buff);
        //     Entity1.BuffComponent.AddBuff(buff, null, Entity1.gameObject, 1, true);
        // }
        //
        // [Button]
        // public void AddAddSpeedOnCrit()
        // {
        //     var buff = new AddAtkSpeedOnCrit(Entity1);
        //
        //     // diContainer.Inject(buff);
        //     Entity1.BuffComponent.AddBuff(buff, null, Entity1.gameObject, 1, true);
        // }

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