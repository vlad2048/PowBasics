# PowBasics Library Collection

## Table of content

- [Introduction](#introduction)
- [PowBasics](#powbasics)
- [PowBasics.Geom](#powbasics.geom)
- [PowBasics.WinForms](#powbasics.winforms)
- [PowBasics.Rx](#powbasics.rx)
- [License](#license)



## Introduction

This is a collection of utility functions I found I often need in my projects.
They are split over 3 packages:
- **PowBasics**
  General utility functions
  (no dependencies)
- **PowBasics.Geom**
  Structs & utils for Rectangles, points, margins...
  (no dependencies)
- **PowBasics.WinForms**
  Utilities for WinForms
  depends on WinForms
- **PowBasics.Rx**
  Utilities for reactive extensions
  depends on System.Reactive



# PowBasics

## Math
```c#
public static class MathUtils
{
    // Cap a number between 2 bounds (inclusive)
    int Cap(int val, int min, int max);

    /*
      Compute the minimum number of multiples of b we need to be at least a
      Note: this is not the same as integer division.
      Integer division: 7 / 3 = 2 (but 2*3=6 doesn't cover/fill 7 completely)
      FillingDiv(7, 3) = 3
    */
    int FillingDiv(int a, int b);
}
```



# PowBasics.Geom
It contains :
- records for points, size, rectangles (both with int & double coordinates)
- records for margins and padding

It has all the operations you'd expect either as member methods or extension methods.
Including:
- **+** & **-** operators between rectangles (**R**) and margins & paddings (**Marg** && **Pad**)
- Mirror operations for margins & paddings. (Swaps x and y coordinates)

```c#
record Pt(int X, int Y);
record R(int X, int Y, int Width, int Height);
record Sz(int Width, int Height);

record VecPt(double X, double Y);
record VecR(VecPt Min, VecPt Max);

enum Dir { Horiz, Vert }

interface IMarg {
    int Top { get; }
    int Right { get; }
    int Bottom { get; }
    int Left { get; }
}

record Marg(int Top, int Right, int Bottom, int Left) : IMarg;
record Pad(int Top, int Right, int Bottom, int Left, int InBetween) : IMarg;

```



# PowBasics.WinForms



# PowBasics.Rx



## License

MIT
