namespace PowBasics.Algorithms;

public static class ConsecutiveSplitter
{
	public static T[][] Split<T>(IEnumerable<T> source, Func<T, T, bool> predicate) =>
		Split(source, (a, b, _) => predicate(a, b));

	public static T[][] Split<T>(IEnumerable<T> source, Func<T, T, int, bool> predicate)
	{
		var arr = source.ToArray();
		var res = new List<T[]>();
		var cur = new List<T>();

		switch (arr.Length)
		{
			case 0:
				return res.ToArray();
			case 1:
				return new[] { arr };
		}


		void AddCur()
		{
			if (cur.Count == 0) return;
			res.Add(cur.ToArray());
			cur.Clear();
		}

		for (var i = 0; i < arr.Length - 1; i++)
		{
			var elt = arr[i];
			var eltNext = arr[i + 1];
			cur.Add(elt);
			if (predicate(elt, eltNext, i))
				AddCur();
		}
		cur.Add(arr.Last());
		AddCur();

		return res.ToArray();
	}
}