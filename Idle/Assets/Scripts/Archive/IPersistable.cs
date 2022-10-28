/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-02-05 21:02:55
 * @modify date 2021-02-05 21:02:55
 * @desc [可持久化对象需继承该接口]
 */

using HM.Extenject;

namespace HM.Interface
{
    public interface IPersistable : IInjectable
    {
        /// <summary>
        /// 存档里的唯一标识符
        /// </summary>
        abstract string PersistKey { get; }

        void OnArchiveWillLoad(IArchive archive);

        void OnArchiveWillSave(IArchive archive);

        /// <summary>
        /// 数据被成功加载后调用
        /// </summary>
        void OnLoaded();

        /// <summary>
        /// 数据即将被保存前调用
        /// </summary>
        void OnWillSave();
    }
}