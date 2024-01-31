# PowDisp

## Usage
```c#
// Create a Disp
var d = new Disp("Name");

// Use extension method to add IDisposable objects to it
IDisposable myObject = new MyObject().D(d);
```

## Check that all Disps were disposed
```c#
// Call this at the end of your program (or test)
DispDiag.CheckForUndisposedDisps();
```

## Config
```c#
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