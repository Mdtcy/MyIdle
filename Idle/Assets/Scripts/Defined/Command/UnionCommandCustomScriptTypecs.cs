/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-10-13 09:51:02
 * @modify date 2022-10-13 09:51:02
 * @desc [UnionCommandType.CustomScript类型，每个类型对应一个脚本]
 */

using System;

namespace NewLife.Defined.Condition
{
    public struct UnionCommandCustomScriptType
    {
        /// <summary>
        /// 类型名
        /// </summary>
        public readonly string Name;

        public UnionCommandCustomScriptType(string name)
        {
            Name = name ?? string.Empty;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"UnionCommandCustomScriptTypeType.{Name}";
        }

        public static bool operator==(UnionCommandCustomScriptType a, UnionCommandCustomScriptType b)
        {
            return a.Name.Equals(b.Name, StringComparison.Ordinal);
        }

        public static bool operator!=(UnionCommandCustomScriptType a, UnionCommandCustomScriptType b)
        {
            return !a.Name.Equals(b.Name, StringComparison.Ordinal);
        }

        public bool Equals(UnionCommandCustomScriptType other)
        {
            return Name == other.Name;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is UnionCommandCustomScriptType other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public static implicit operator UnionCommandCustomScriptType(string someValue)
        {
            return string.IsNullOrEmpty(someValue) ? NotSpecified : new UnionCommandCustomScriptType(someValue);
        }

        #region Defined

        public static readonly UnionCommandCustomScriptType NotSpecified   = new UnionCommandCustomScriptType("NotSpecified");   // 未指定

        #endregion
    }
}