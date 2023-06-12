﻿namespace PowBasics.EqualityCode;

public static class EqualityUtils
{
	public static IEqualityComparer<T> Make<T, K>(Func<T, K> fun) => Make<T>(
		e =>
		{
			var k = fun(e);
			return k == null ? 0 : k.GetHashCode();
		},
		(a, b) =>
		{
			var (fa, fb) = (fun(a), fun(b));
			return (fa, fb) switch
			{
				(not null, not null) => Equals(fa, fb),
				(null, null) => true,
				_ => false
			};
		}
	);

	public static IEqualityComparer<T> Make<T>(
		Func<T, int> getHashCode,
		Func<T, T, bool> equals
	)
	{
		ArgumentNullException.ThrowIfNull(getHashCode);
		ArgumentNullException.ThrowIfNull(equals);
		return new Comparer<T>(getHashCode, equals);
	}

	private sealed class Comparer<T> : IEqualityComparer<T>
	{
		private readonly Func<T, int> getHashCode;
		private readonly Func<T, T, bool> equals;
		public Comparer(Func<T, int> getHashCode, Func<T, T, bool> equals)
		{
			this.getHashCode = getHashCode;
			this.equals = equals;
		}
		public bool Equals(T? x, T? y) => (x, y) switch
		{
			(not null, not null) => equals(x, y),
			(null, null) => true,
			_ => false
		};
		public int GetHashCode(T obj) => getHashCode(obj);
	}
}