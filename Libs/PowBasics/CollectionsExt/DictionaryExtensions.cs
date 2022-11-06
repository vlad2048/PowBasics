namespace PowBasics.CollectionsExt;

public static class DictionaryExtensions
{
	public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> createFun)
		where TKey : notnull
	{
		if (!dict.TryGetValue(key, out var val))
			val = dict[key] = createFun();
		return val;
	}
}