/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-22 17:06:18
 * @modify date 2020-06-22 17:06:18
 * @desc [Generate unique id.]
 */

namespace NewLife.BusinessLogic.UniqueId
{
    public interface IIdGenerator<out T>
    {
        /// <summary>
        /// 生成一个新的id
        /// </summary>
        /// <returns></returns>
        T Generate();
    }
}