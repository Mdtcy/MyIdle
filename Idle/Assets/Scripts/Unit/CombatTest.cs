/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc []
 */

#pragma warning disable 0649
using System;
using IdleGame.Buff;
using IdleGame.Buff.BuffModels;
using IdleGame.Buff.BuffModels.PoisonSkills;
using QFramework;
using QFramework.Example;
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

        // [Button]
        // public void AddAddCriticalProbabilityWhenHpLower30ToEntity1()
        // {
        //     var buff = new AddBuffInfo(new Add)
        //
        //     // diContainer.Inject(buff);
        //     Entity1.BuffComponent.AddBuff(buff, null, Entity1.gameObject ,1, true);
        // }

        public int stack = 10;

        public Entity Entity;

        [Button]
        public void AddAddAtkSpeedBuff()
        {
            var addBuffInfo = new AddBuffInfo(
                                              new AddAtkSpeedBuffModel(),
                                              null,
                                              null,
                                              stack,
                                              5f,
                                              true,
                                              true,
                                              null);

            // diContainer.Inject(buff);
            Entity.BuffComponent.AddBuff(addBuffInfo);
        }

        [Button]
        public void AddAddAtkBuff()
        {
            var addBuffInfo = new AddBuffInfo(
                                              new AddAtkBuffModel(),
                                              null,
                                              null,
                                              stack,
                                              5f,
                                              true,
                                              true,
                                              null);

            // diContainer.Inject(buff);
            Entity.BuffComponent.AddBuff(addBuffInfo);
        }

        [Button]
        public void AddAddCriticalChanceBuff()
        {
            var addBuffInfo = new AddBuffInfo(
                                              new AddCriticalChanceBuffModel(),
                                              null,
                                              null,
                                              stack,
                                              5f,
                                              true,
                                              true,
                                              null);

            // diContainer.Inject(buff);
            Entity.BuffComponent.AddBuff(addBuffInfo);
        }

        [Button]
        public void AddAddCriticalDamageBuff()
        {
            var addBuffInfo = new AddBuffInfo(
                                              new AddCriticalDamageBuffModel(),
                                              null,
                                              null,
                                              stack,
                                              5f,
                                              true,
                                              true,
                                              null);

            // diContainer.Inject(buff);
            Entity.BuffComponent.AddBuff(addBuffInfo);
        }

        [Button]
        public void AddHealOverTimePoint3()
        {
            var addBuffInfo = new AddBuffInfo(
                                              new HealOverTimePoint3(),
                                              null,
                                              null,
                                              stack,
                                              5f,
                                              true,
                                              true,
                                              null);

            // diContainer.Inject(buff);
            Entity.BuffComponent.AddBuff(addBuffInfo);
        }

        [Button]
        public void AddStun()
        {
            var addBuffInfo = new AddBuffInfo(
                                              new StunBuffModel(),
                                              null,
                                              null,
                                              1,
                                              3f,
                                              true,
                                              false,
                                              null);

            // diContainer.Inject(buff);
            Entity.BuffComponent.AddBuff(addBuffInfo);
        }

        [Button]
        public void AddPoison()
        {
            var addBuffInfo = new AddBuffInfo(
                                              new PoisonBuffModel(),
                                              Entity.gameObject,
                                              Entity.gameObject,
                                              1,
                                              3f,
                                              true,
                                              true,
                                              null);

            // diContainer.Inject(buff);
            Entity.BuffComponent.AddBuff(addBuffInfo);
        }



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

        [Button]
        public void TestSelectSkill()
        {
            UIKit.OpenPanel<UISelectSkill>();
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