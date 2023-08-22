namespace PowBasics.CollectionsExt;

public static class IEnumerableShuffleExt
{
	public static T[] Shuffle<T>(this IEnumerable<T> source, int? seed)
	{
		var rnd = seed switch
		{
			not null => new Random(seed.Value),
			null => new Random((int)DateTime.Now.Ticks)
		};
		var array = source.ToArray();
		var n = array.Length;
		for (var i = 0; i < (n - 1); i++)
		{
			var r = i + rnd.Next(n - i);
			(array[r], array[i]) = (array[i], array[r]);
		}
		return array;
	}
}