using System;

namespace HM
{
	public class Tuple<T1, T2, T3> : IEquatable<Object>
	{
		public T1 Value1
		{
			get;
			set;
		}

		public T2 Value2
		{
			get;
			set;
		}

		public T3 Value3
		{
			get;
			set;
		}

		public Tuple(T1 Item1, T2 Item2, T3 Item3)
		{
			this.Value1 = Item1;
			this.Value2 = Item2;
			this.Value3 = Item3;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || (obj as Tuple<T1, T2, T3>) == null) //if the object is null or the cast fails
				return false;
			Tuple<T1, T2, T3> tuple = (Tuple<T1, T2, T3>)obj;
			return Value1.Equals(tuple.Value1) && Value2.Equals(tuple.Value2) && Value3.Equals(tuple.Value3);
		}

		public override int GetHashCode()
		{
			return Value1.GetHashCode() ^ Value2.GetHashCode() ^ Value3.GetHashCode();
		}

		public static bool operator ==(Tuple<T1, T2, T3> tuple1, Tuple<T1, T2, T3> tuple2)
		{
			return tuple1.Equals(tuple2);
		}

		public static bool operator !=(Tuple<T1, T2, T3> tuple1, Tuple<T1, T2, T3> tuple2)
		{
			return !tuple1.Equals(tuple2);
		}
	}
}