namespace PowBasics.MathCode;

public static class MathUtils
{
	/// <summary>
	/// Caps an number between two bounds (inclusive)
	/// </summary>
	/// <param name="val">Number to cap</param>
	/// <param name="min">Minimum bound</param>
	/// <param name="max">Maximum bound</param>
	/// <returns>Capped number</returns>
	/// <exception cref="ArgumentException">If max &lt; min</exception>
	public static int Cap(int val, int min, int max)
	{
		if (max < min) throw new ArgumentException();
		if (val < min)
			val = min;
		if (val > max)
			val = max;
		return val;
	}

	/// <summary>
	/// Compute the minimum number of multiples of b we need to be at least a
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException">If b == 0</exception>
	public static int FillingDiv(int a, int b)
	{
		if (b == 0) throw new ArgumentException();
		if (a < 0)
			return 0;
		var res = (a - 1) / b + 1;
		return res;
	}
}