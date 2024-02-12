# PowDisp

## Usage
```c#
// Create a Disp
var d = new Disp("Name");

// Use extension method to add IDisposable objects to it
var myObject = new MyDisposableObject().D(d);
```

## Check that all Disps were disposed
```c#
// Call this at the end of your program (or test)
DispDiag.CheckForUndisposedDisps();
```

## Config
```c#
// If you want Disps to dispose of their IDisposables in reverse order set:
DispDiag.DisposeInReverseOrder = true;
```

## Unit Tests
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