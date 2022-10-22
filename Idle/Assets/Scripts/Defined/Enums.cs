/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-25 23:05:07
 * @modify date 2020-05-25 23:05:07
 * @desc [description]
 */

namespace NewLife.Defined
{
    /// <summary>
    /// 条件类型
    /// </summary>
    public enum ConditionType : short
    {
        Unspecified   = 0, // 尚未指定
    }

    /// <summary>
    /// 命令类型
    /// </summary>
    public enum CommandType : short
    {
        Unspecified       = 0, // 未指定
        AcquireItem       = 1, // 获得(解锁)物品
        CustomScript      = 5, // 自定义脚本命令
    }
}