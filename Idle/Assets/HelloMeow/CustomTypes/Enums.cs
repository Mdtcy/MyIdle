/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-16 17:48:19
 * @modify date 2018-12-16 17:48:19
 * @desc [项目所有枚举都在此文件中定义]
 */
namespace HM.GameBase
{
    /// <summary>
    /// Item状态
    /// </summary>
    public enum ItemState
    {
        NotSpecified,
        Default,
        Equipped,
        Finished, // 用于一些可完成的物品(任务、成就)
    }

    /// <summary>
    /// 逻辑关系
    /// </summary>
    public enum LogicalRelation
    {
	    And,	// 与
	    Or,		// 或
    }
}