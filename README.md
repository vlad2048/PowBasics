# PowBasics

## Table of content

- [Introduction](#introduction)
- [PowBasics](#powbasics)
- [PowBasics.WinForms](#powbasics.winforms)
- [PowBasics.Rx](#powbasics.rx)
- [License](#license)



## Introduction

This is a collection of utility functions I found I often need in my projects.
They are split over 3 packages:
- **PowBasics**
  no dependencies
- **PowBasics.WinForms**
  depends on WinForms
- **PowBasics.Rx**
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



# PowBasics.WinForms



# PowBasics.Rx



## License

MIT
