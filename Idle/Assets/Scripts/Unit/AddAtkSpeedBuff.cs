// /**
//  * @author BoLuo
//  * @email [ tktetb@163.com ]
//  * @create date  2023年1月6日
//  * @modify date 2023年1月6日
//  * @desc []
//  */
//
// #pragma warning disable 0649
// using IdleGame;
// using Numeric;
//
// namespace Unit
// {
//     public class Add100AtkSpeedBuff : Buff
//     {
//         private Entity owener;
//
//         private float attackSpeedChanged;
//
//         public override string Id()
//         {
//             return "AddAtkSpeedBuff";
//         }
//
//         public Add100AtkSpeedBuff(Entity entity)
//         {
//             owener = entity;
//         }
//
//         public override void OnOccur(int modStack)
//         {
//             attackSpeedChanged = 100 * modStack;
//             owener.ModifyAttribute(AttributeType.AttackSpeed, 100 * modStack, ModifyNumericType.Add);
//         }
//     }
// }
// #pragma warning restore 0649