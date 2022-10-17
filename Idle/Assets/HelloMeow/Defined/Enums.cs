/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-28 20:03:34
 * @modify date 2020-03-28 20:03:34
 * @desc [description]
 */

namespace HM.Defined
{
    /// <summary>
    /// 数字关系枚举类型
    /// </summary>
    public enum NumericRelation
    {
        GreaterThan, // >
        GreaterThanOrEqual, // >=
        LessThan, // <
        LessThanOrEqual, // <=
        Equal, // ==
    }

    public enum AnchorPointType
    {
	    Custom,
	    BottomLeft,
	    BottomCenter,
	    BottomRight,
	    CenterLeft,
	    Center,
	    CenterRight,
	    TopLeft,
	    TopCenter,
	    TopRight
    }

    /// <summary>
	/// 方向
	/// </summary>
	public enum Direction
	{
		Left,	// 左
		Right,	// 右
		Up,		// 上
		Down,	// 下
	}
}