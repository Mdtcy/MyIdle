/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-30 17:03:27
 * @modify date 2020-03-30 17:03:27
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using HM.Extensions;

namespace HM.GameBase
{
    public class ListCollection<T1, T2> :
	    IDisposable
	    where T1 : class, T2
	    where T2 : class
    {
        protected List<T2> configs;

        public virtual void Dispose()
        {
	        if (configs != null)
	        {
		        ListPool<T2>.Release(configs);
	        }
        }

        public int Count => configs.IsNullOrEmpty() ? 0 : configs.Count;

        public List<T2> Configs => configs;

        public T1 this[int i]
        {
	        get
	        {
		        if (configs == null || i < 0 || i >= configs.Count)
		        {
			        return default;
		        }
		        return configs[i] as T1;
	        }
        }

        protected virtual void Prepare(int opacity)
        {
	        // Claim固定GC Alloc（除首次外，测试300B左右）
            configs = ListPool<T2>.Claim(opacity);
        }

        #region Enumerator

        public struct FastEnumerator
		{
			public bool MoveNext()
			{
				i++;
				return i < values.Count;
			}

			public T1 Current => values[i] as T1;

			public FastEnumerator(List<T2> items)
			{
				values = items;
				i = -1;
			}

			private readonly List<T2> values;
			private int i;
		}

		public FastEnumerator GetEnumerator()
		{
			return new FastEnumerator(configs);
		}

		#endregion
    }
}