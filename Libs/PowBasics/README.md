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