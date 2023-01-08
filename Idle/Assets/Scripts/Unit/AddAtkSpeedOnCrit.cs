// /**
//  * @author BoLuo
//  * @email [ tktetb@163.com ]
//  * @create date  2023年1月7日
//  * @modify date 2023年1月7日
//  * @desc [任务组UI]
//  */
//
// #pragma warning disable 0649
// using IdleGame;
//
// namespace Unit
// {
//     /// <summary>
//     /// 暴击时＋攻速，但是此时攻速也会增加暴击出现的次数，这是一个叠起来就无限叠的属性，不太好控制
//     /// </summary>
//     public class AddAtkSpeedOnCrit : Buff
//     {
//         private Entity owener;
//
//         private float atkSpeedPerStark;
//
//         public AddAtkSpeedOnCrit(Entity entity)
//         {
//             owener = entity;
//         }
//
//         public override void OnOccur(int modStack)
//         {
//             atkSpeedPerStark = 50 * Stack;
//         }
//
//         // public void Trigger(EEventOnCrit t)
//         // {
//         //     float change = atkSpeedPerStark;
//         //     ActionKit.Sequence()
//         //              .Callback(() =>
//         //               {
//         //                   owener.ModifyAttribute(AttributeType.AttackSpeed, change, ModifyNumericType.Add);
//         //               })
//         //              .Delay(2f)
//         //              .Callback(() =>
//         //               {
//         //                   owener.ModifyAttribute(AttributeType.AttackSpeed, -change, ModifyNumericType.Add);
//         //               })
//         //              .Start(owener);
//         // }
//
//         public override string Id()
//         {
//             return "AddAtkSpeedOnCrit";
//         }
//     }
// }
// #pragma warning restore 0649