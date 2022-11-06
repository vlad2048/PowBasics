namespace PowBasics.CollectionsExt;

public static class IEnumerableExt
{
	public static string JoinText<T>(this IEnumerable<T> source, string separator = ";") => string.Join(separator, source);

	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (var elt in source)
			action(elt);
	}

	public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
	{
		var index = 0;
		foreach (var elt in source)
			action(elt, index++);
	}

	public static IEnumerable<(T1, T2, T3, T4)> Zip<T1, T2, T3, T4>(
		this IEnumerable<T1> source,
		IEnumerable<T2> second,
		IEnumerable<T3> third,
		IEnumerable<T4> fourth
	)
	{
		using var e1 = source.GetEnumerator();
		using var e2 = second.GetEnumerator();
		using var e3 = third.GetEnumerator();
		using var e4 = fourth.GetEnumerator();
		while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
			yield return (e1.Current, e2.Current, e3.Current, e4.Current);
	}

	public static HashSet<U> ToHashSet<T, U>(this IEnumerable<T> source, Func<T, U> fun) => source.Select(fun).ToHashSet();


	// ********************
	// * Where Extensions *
	// ********************
	public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source.Where(e => !predicate(e));
	public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, int, bool> predicate) => source.Select((e, i) => (e, i)).Where(t => !predicate(t.e, t.i)).Select(t => t.e);

	public static T[] WhereToArray<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source.Where(predicate).ToArray();
	public static T[] WhereToArray<T>(this IEnumerable<T> source, Func<T, int, bool> predicate) => source.Select((e, i) => (e, i)).Where(t => predicate(t.e, t.i)).Select(t => t.e).ToArray();

	public static T[] WhereNotToArray<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source.WhereNot(predicate).ToArray();
	public static T[] WhereNotToArray<T>(this IEnumerable<T> source, Func<T, int, bool> predicate) => source.Select((e, i) => (e, i)).WhereNot(t => predicate(t.e, t.i)).Select(t => t.e).ToArray();

	public static List<T> WhereToList<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source.Where(predicate).ToList();
	public static List<T> WhereToList<T>(this IEnumerable<T> source, Func<T, int, bool> predicate) => source.Select((e, i) => (e, i)).Where(t => predicate(t.e, t.i)).Select(t => t.e).ToList();

	public static List<T> WhereNotToList<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source.WhereNot(predicate).ToList();
	public static List<T> WhereNotToList<T>(this IEnumerable<T> source, Func<T, int, bool> predicate) => source.Select((e, i) => (e, i)).WhereNot(t => predicate(t.e, t.i)).Select(t => t.e).ToList();


	// *********************
	// * Select Extensions *
	// *********************
	public static U[] SelectToArray<T, U>(this IEnumerable<T> source, Func<T, U> fun) => source.Select(fun).ToArray();
	public static U[] SelectToArray<T, U>(this IEnumerable<T> source, Func<T, int, U> fun) => source.Select(fun).ToArray();

	public static List<U> SelectToList<T, U>(this IEnumerable<T> source, Func<T, U> fun) => source.Select(fun).ToList();
	public static List<U> SelectToList<T, U>(this IEnumerable<T> source, Func<T, int, U> fun) => source.Select(fun).ToList();

	public static IReadOnlyList<U> SelectToReadOnlyList<T, U>(this IEnumerable<T> source, Func<T, U> fun) => source.Select(fun).ToList();
	public static IReadOnlyList<U> SelectToReadOnlyList<T, U>(this IEnumerable<T> source, Func<T, int, U> fun) => source.Select(fun).ToList();


	// ************************
	// * GetHashCodeAggregate *
	// ************************
	public static int GetHashCodeAggregate<T>(this IEnumerable<T> source) where T : notnull => source.GetHashCodeAggregate(17);
	private static int GetHashCodeAggregate<T>(this IEnumerable<T> source, int hash) where T : notnull => source.Aggregate(hash, (current, item) => current * 31 + item.GetHashCode());
}