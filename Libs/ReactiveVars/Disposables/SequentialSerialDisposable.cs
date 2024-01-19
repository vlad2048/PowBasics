using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace ReactiveVars;

public sealed class SequentialSerialDisposable : IDisposable
{
	private readonly SerialDisposable serD = new();
	public void Dispose() => serD.Dispose();

	private Func<IDisposable>? disposableFun;

	public Func<IDisposable>? DisposableFun
	{
		get => disposableFun;
		set
		{
			CurrentThreadScheduler.Instance.Schedule(() =>
			{
				disposableFun = value;
				serD.Disposable = null;
				if (disposableFun != null)
				{
					serD.Disposable = disposableFun();
				}
			});
		}
	}
}