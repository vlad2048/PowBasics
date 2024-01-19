# ReactiveVars

## Usings
```c#
global using LanguageExt;
global using LanguageExt.Common;
global using static LanguageExt.Prelude;
global using Unit = LanguageExt.Unit;

global using Obs = System.Reactive.Linq.Observable;
global using Disp = System.Reactive.Disposables.CompositeDisposable;
global using static ReactiveVars.DispMaker;

global using LR = ReactiveVars.ReactiveVarsLogger;
```

## Disp Tracking
```c#
void Main() {
	var d = MkD("Name");
	LogAndTellIfThereAreUndisposedDisps();	// in DispMaker
}
```

## Tests
```c#
[SetUp] void Setup() {
	ResetDispsForTests();
}
[TearDown] void Teardown() {
	LogAndTellIfThereAreUndisposedDisps();
}
```

## Config Tracker
```c#
public record struct CfgLog(
	bool WinGeom,
	bool NcHitTest
);

public record struct Cfg(
	CfgLog Log
);

public static class G
{
	private const string ConfigFile = @"config\config.json";

	private static readonly Disp D = new();
	private static readonly IRwVar<IRoVar<Cfg>> CfgVar = Var.Make(RxCfg.Make(ConfigFile, default(Cfg), SysJsoner.Config), D);

	static G()
	{
		var schedD = new ScheduledDisposable(TaskPoolScheduler.Default, D);
		AppDomain.CurrentDomain.ProcessExit += (_, _) =>
		{
			schedD.Dispose();
		};
	}

	public static IRoVar<Cfg> Cfg => CfgVar.Switch().ToVar();
}
```
