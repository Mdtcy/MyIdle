/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-08-22 12:08:10
 * @modify date 2019-08-22 12:08:10
 * @desc [存档管理器(内存模式，重进游戏则恢复新用户状态)]
 */
using HM.Interface;

namespace HM
{
    /// <summary>
    /// 存档管理器(内存模式，重进游戏则恢复新用户状态)
    /// </summary>
    public class MemoryArchive : IArchive
    {
        /// <inheritdoc />
        public void Register(IArchiveClient itemToArchive)
        {
        }

        /// <inheritdoc />
        public void Save()
        {
        }

        /// <inheritdoc />
        public void Load(string name)
        {
        }

        /// <inheritdoc />
        public bool ArchiveExists(string name)
        {
            return false;
        }

        /// <summary>
        /// 创建并加载存档
        /// </summary>
        /// <param name="name"></param>
        public void CreateAndLoad(string name)
        {
        }

        /// <summary>
        /// 加载最近一次使用的存档
        /// </summary>
        public void Load()
        {
        }

        /// <summary>
        ///  加载任何存档前可利用该函数检查是否有最近使用过的存档
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            // 永远创建新用户
            return false;
        }

        /// <summary>
        /// 加载指定数据到对象（覆盖）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        public void LoadInto<T>(string key, T target) where T : class
        {
        }

        /// <summary>
        /// 保存数据到存档里(key-value形式)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void Save<T>(string key, T value)
        {
        }

        /// <inheritdoc />
        public void Backup(string name)
        {
        }

        /// <inheritdoc />
        public bool   IsEnabled          { get; set; }

        /// <inheritdoc />
        public string CurrentArchivePath { get; }

        /// <inheritdoc />
        public ES3File CurrentFile { get; }

        /// <inheritdoc />
        public bool IsValidArchive(string filepath)
        {
            return true;
        }

        /// <inheritdoc />
        public void ReplaceArchive(string newArchivePath, string name)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public string Export()
        {
            return string.Empty;
        }
    }
}