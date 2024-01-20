using System.Text.Json.Serialization;

namespace PowBasics.Geom;

public readonly record struct Sz(int Width, int Height)
{
	[JsonIgnore]
	public bool IsDegenerate => Width == 0 || Height == 0;
	public static readonly Sz Empty = new(0, 0);

	public override string ToString() => $"{Width}x{Height}";

	public static Sz operator +(Sz a, Sz b) => new(a.Width + b.Width, a.Height + b.Height);
	public static Sz operator -(Sz a, Sz b) => new(Math.Max(0, a.Width - b.Width), Math.Max(0, a.Height - b.Height));
	public static Sz operator *(Sz a, int z) => new(a.Width * z, a.Height * z);
	public static Sz operator *(Sz a, double z) => new((int)(a.Width * z), (int)(a.Height * z));
	public static Sz operator /(Sz a, double z) => new((int)(a.Width / z), (int)(a.Height / z));
}
