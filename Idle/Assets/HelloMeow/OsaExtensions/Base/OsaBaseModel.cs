/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-11-25 14:11:20
 * @modify date 2020-11-25 14:11:20
 * @desc [在此定义ui需要的数据]
 */

using System;

namespace HM.OsaExtensions
{
    public class OsaBaseModel
    {
        /// <summary>
        /// Model类型
        /// </summary>
        public Type CachedType { get; }

        /// <summary>
        /// 数据是否有变化（根据该值决定是否重绘UI）
        /// </summary>
        public bool IsDirty;

        public OsaBaseModel()
        {
            CachedType = GetType();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"isDirty = {IsDirty}, cachedType = {CachedType}";
        }
    }
}