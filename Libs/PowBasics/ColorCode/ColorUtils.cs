using System.Drawing;
using PowBasics.CollectionsExt;
using PowBasics.RandomCode;

namespace PowBasics.ColorCode;

public static class ColorUtils
{
	private const double MaxHue = 360;

	public static Color[] MakePalette(int count, int? seed = null, double sat = 0.72, double val = 0.58)
	{
		var rnd = RndUtils.Make(seed);
		var start = rnd.NextDouble() * MaxHue;
		return SplitHueInterval(count, start)
			.SelectToArray(hue => ColorFromHSV(hue, sat, val));
	}

	private static IEnumerable<double> SplitHueInterval(int count, double start) =>
		Enumerable.Range(0, count)
			.Select(i => start + i * (MaxHue / count))
			.Select(NormalizeHue);

	private static double NormalizeHue(double hue)
	{
		while (hue > MaxHue)
			hue -= MaxHue;
		return hue;
	}

	private static Color ColorFromHSV(double hue, double saturation, double value)
	{
		var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
		var f = hue / 60 - Math.Floor(hue / 60);

		value *= 255;
		var v = Convert.ToInt32(value);
		var p = Convert.ToInt32(value * (1 - saturation));
		var q = Convert.ToInt32(value * (1 - f * saturation));
		var t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

		static Color Mk(int r, int g, int b) => Color.FromArgb(255, r, g, b);

		return hi switch
		{
			0 => Mk(v, t, p),
			1 => Mk(q, v, p),
			2 => Mk(p, v, t),
			3 => Mk(p, q, v),
			4 => Mk(t, p, v),
			_ => Mk(v, p, q)
		};
	}
}