/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-02-10 14:25:16
 * @modify date 2022-02-10 14:25:16
 * @desc [导出对话树为json给小程序端使用，有些对话树节点需特殊处理，需实现该接口]
 */

namespace NewLife.Defined.MiniProgram
{
    public interface IMiniProgramJsonClient
    {
        /// <summary>
        /// 导出前
        /// </summary>
        void OnWillExport();
    }
}