# PowDisp

## Usings
```c#
global using Obs = System.Reactive.Linq.Observable;
global using static PowDisp.DispMaker;
```

## Disp Tracking
```c#
// Create disps (this function is in the DispMaker class)
var d = MkD("Name");

// Call this at the end of your program (or test)
DispDiag.CheckForUndisposedDisps();
```

## Config
```c#
// Makes Disp dispose of its contained IDisposables in reverse order
DispDiag.DisposeInReverseOrder = true;
```

## Tests
```c#
[SetUp] void Setup() {
	Reseter.ResetDispsForTests();
}
[TearDown] void Teardown() {
	DispDiag.CheckForUndisposedDisps();
}
```

## Logging
```c#
// Will print every Disp created and disposed to the console
DispDiag.DispMakerLoggingEnabled = true;

// Returns stats about the number of created and disposed Disps
var stats = DispDiag.GetStats();
Console.WriteLine($"Disps created : {stats.Created}");
Console.WriteLine($"Disps disposed: {stats.Disposed}");
```