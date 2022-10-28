/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-08-19 19:43:06
 * @modify date 2019-08-19 19:43:06
 * @desc [description]
 */
namespace HM.Interface
{
    /// <summary>
    /// 存档接口
    /// </summary>
    public interface IArchive
    {
        /// <summary>
        /// 检查指定存档是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool ArchiveExists(string name);

        /// <summary>
        /// 注册需要保存的对象
        /// </summary>
        /// <param name="itemToArchive"></param>
        void Register(IPersistable itemToArchive);

        void Save();

        /// <summary>
        /// 加载指定存档
        /// </summary>
        void Load(string name);

        /// <summary>
        /// 加载任何存档前可利用该函数检查是否有最近使用过的存档
        /// </summary>
        /// <returns></returns>
        bool Exists();

        /// <summary>
        /// 加载指定数据到对象（覆盖）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        void LoadInto<T>(string key, T target) where T : class;

        /// <summary>
        /// 保存数据到存档里(key-value形式)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        void Save<T>(string key, T value);

        /// <summary>
        /// 备份当前存档（调试用）
        /// </summary>
        /// <param name="name"></param>
        void Backup(string name);

        /// <summary>
        /// 如果为false，则不会保存
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 当前存档路径
        /// </summary>
        string CurrentArchivePath { get; }

        /// <summary>
        /// 当前存档File
        /// </summary>
        /// <returns></returns>
        ES3File CurrentFile { get; }

        /// <summary>
        /// 是否合法存档
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        bool IsValidArchive(string filepath);

        /// <summary>
        /// 替换存档文件
        /// </summary>
        /// <param name="newArchivePath"></param>
        /// <param name="name"></param>
        void ReplaceArchive(string newArchivePath, string name);

        /// <summary>
        /// 获取当前存档内容
        /// </summary>
        /// <returns></returns>
        string Export();
    }
}