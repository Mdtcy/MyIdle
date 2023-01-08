/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月8日
 * @modify date 2023年1月8日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
namespace Unit
{
    public class ChaControlState
    {
        // todo 现在还没应用 之后可以移动的敌人需要使用这个
        ///<summary>
        ///是否可以移动坐标
        ///</summary>
        public bool canMove;

        ///<summary>
        ///是否可以攻击
        ///</summary>
        public bool canAttack;

        public ChaControlState(bool canMove = true, bool canAttack = true)
        {
            this.canMove   = canMove;
            this.canAttack = canAttack;
        }

        public void Origin()
        {
            this.canMove   = true;
            this.canAttack = true;
        }


        public static ChaControlState Orgin = new ChaControlState(true, true);

        ///<summary>
        ///昏迷效果
        ///</summary>
        public static ChaControlState Stun = new ChaControlState(false, false);

        public static ChaControlState operator +(ChaControlState cs1, ChaControlState cs2)
        {
            return new ChaControlState(
                                       cs1.canMove   & cs2.canMove,
                                       cs1.canAttack & cs2.canAttack
                                      );
        }

    }
}
#pragma warning restore 0649