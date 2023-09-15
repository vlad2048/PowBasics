using PowBasics.CollectionsExt;
using System.Collections.Concurrent;

namespace PowBasics.Concurrency;

public static class Parallelize
{
	public static U[] Run<T, U>(T[] arr, int maxParallelism, Func<T, Task<U>> fun)
	{
		var queue = MakeQueue(arr.Length);
		var res = new U[arr.Length];
		var tasks = Enumerable
			.Range(0, maxParallelism)
			.SelectToArray(_ => Task.Run(async () =>
			{
				while (queue.TryDequeue(out var idx))
					res[idx] = await fun(arr[idx]);
			}));
		Task.WaitAll(tasks);
		return res;
	}
	
	private static ConcurrentQueue<int> MakeQueue(int count) => new(Enumerable.Range(0, count));
}