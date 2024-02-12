using System.Reactive.Disposables;
using PowDisp;

namespace ReactiveVars;

public sealed class SerDisp : IDisposable
{
	public void Dispose() => serD.Dispose();

	private readonly SerialDisposable serD = new();

	public Disp GetNewD()
	{
		serD.Disposable = null;
		var d = new Disp("SerDisp");
		serD.Disposable = d;
		return d;
	}
}