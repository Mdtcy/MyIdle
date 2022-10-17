/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-08 14:43:48
 * @modify date 2022-09-08 14:43:48
 * @desc [公告]
 */

using System;

namespace NewLife.Defined
{
    public class Notice
    {
        /// <summary>
        /// 公告内容
        /// </summary>
        public string Content;

        /// <summary>
        /// 显示次序(数字越小，越优先)
        /// </summary>
        public int Order;

        /// <summary>
        /// 开始生效日期
        /// </summary>
        public DateTime BeginDate;

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime ExpireDate;


        /// <inheritdoc />
        public override string ToString()
        {
            return $"[Notice {Order} / {BeginDate} - {ExpireDate} / {Content}";
        }

    }
}