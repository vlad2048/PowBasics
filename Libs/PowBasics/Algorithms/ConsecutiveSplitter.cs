namespace PowBasics.Algorithms;

public static class ConsecutiveSplitter
{
	public static List<List<T>> Split<T>(List<T> list, Func<(int, T), (int, T), bool> predicate)
	{
		var res = new List<List<T>>();
		var cur = new List<T>();

		if (list.Count == 0)
			return res;
		if (list.Count == 1)
		{
			res.Add(list);
			return res;
		}

		void AddCur()
		{
			if (cur.Count > 0)
			{
				res.Add(cur.ToList());
				cur.Clear();
			}
		}

		for (var i = 0; i < list.Count - 1; i++)
		{
			var elt = list[i];
			var eltNext = list[i + 1];
			cur.Add(elt);
			if (predicate((i, elt), (i + 1, eltNext)))
			{
				AddCur();
			}
		}
		cur.Add(list.Last());
		AddCur();

		return res;
	}
}