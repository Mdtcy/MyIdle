/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月5日
 * @modify date 2023年1月5日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections.Generic;

namespace Numeric
{
    public class NumericBase<T> where T : System.Enum
    {
        private readonly Dictionary<T, float> numerics = new();

        public float Get(T attributeType)
        {
            return numerics[attributeType];
        }

        public void Set(T attributeType, float value)
        {
            numerics[attributeType] = value;
        }

        public void Add(T attributeType, float value)
        {
            numerics[attributeType] = Get(attributeType) + value;
        }
    }
}
#pragma warning restore 0649