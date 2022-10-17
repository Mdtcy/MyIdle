/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-04-30 21:04:28
 * @modify date 2020-04-30 21:04:28
 * @desc [description]
 */

using System.Collections.Generic;

namespace HM.GameBase
{
    public struct ListFastEnumerator<T>
    {
        public bool MoveNext()
        {
            i++;
            return i < values.Count;
        }

        public T Current => values[i];

        public ListFastEnumerator(List<T> items)
        {
            values = items;
            i = -1;
        }

        private readonly List<T> values;
        private int i;
    }
}