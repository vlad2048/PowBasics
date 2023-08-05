namespace PowBasics.CollectionsExt;

public static class DictionaryExtensions
{
	/// <summary>
	/// Map the keys in a Dictionary
	/// </summary>
	public static IReadOnlyDictionary<K2, V> MapKeys<K1, K2, V>(this IReadOnlyDictionary<K1, V> dict, Func<K1, K2> fun)
		where K1 : notnull
		where K2 : notnull
	{
		var res = new Dictionary<K2, V>();
		foreach (var (key, val) in dict)
			res[fun(key)] = val;
		return res;
	}

	/// <summary>
	/// Map the values in a Dictionary
	/// </summary>
	public static IReadOnlyDictionary<K, V2> MapValues<K, V1, V2>(this IReadOnlyDictionary<K, V1> dict, Func<V1, V2> fun)
		where K : notnull
	{
		var res = new Dictionary<K, V2>();
		foreach (var (key, val) in dict)
			res[key] = fun(val);
		return res;
	}


	/// <summary>
	/// Map the keys in a Dictionary using a lookup map. <br/>
	/// Throws an exception if a key is not found <br/>
	/// </summary>
	public static IReadOnlyDictionary<K2, V> MapKeys<K1, K2, V>(this IReadOnlyDictionary<K1, V> dict, IReadOnlyDictionary<K1, K2> lookupMap)
		where K1 : notnull
		where K2 : notnull
		=>
			dict.MapKeys(e => lookupMap[e]);

	/// <summary>
	/// Map the values in a Dictionary using a lookup map. <br/>
	/// Throws an exception if a value is not found <br/>
	/// </summary>
	public static IReadOnlyDictionary<K, V2> MapValues<K, V1, V2>(this IReadOnlyDictionary<K, V1> dict, IReadOnlyDictionary<V1, V2> lookupMap)
		where K : notnull
		=>
			dict.MapValues(e => lookupMap[e]);


	/// <summary>
	/// Get a value from a Dictionary based on a key, if it doesn't exist, create and add it using the function provided
	/// </summary>
	public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> createFun)
		where TKey : notnull
	{
		if (!dict.TryGetValue(key, out var val))
			val = dict[key] = createFun();
		return val;
	}

	public static void Add<K, V>(this Dictionary<K, List<V>> map, K key, V val) where K : notnull
	{
		if (!map.TryGetValue(key, out var list))
			list = map[key] = new List<V>();
		list.Add(val);
	}
}