using System.Text.Json.Serialization;

namespace PowBasics.Geom;

public readonly record struct Pt(int X, int Y)
{
	public static readonly Pt Empty = new(0, 0);
	public override string ToString() => $"{X},{Y}";
	public bool Equals(Pt other) => X == other.X && Y == other.Y;
	public override int GetHashCode() => HashCode.Combine(X, Y);
	public static Pt operator +(Pt a, Pt b) => new(a.X + b.X, a.Y + b.Y);
	public static Pt operator -(Pt a, Pt b) => new(a.X - b.X, a.Y - b.Y);
	public static Pt operator -(Pt a) => new(-a.X, -a.Y);
	public static Pt operator +(Pt a, Marg m) => new(a.X + m.Left, a.Y + m.Top);
	public static Pt operator -(Pt a, Marg m) => new(a.X - m.Left, a.Y - m.Top);

	[JsonIgnore]
	public double Length => Math.Sqrt(X * X + Y * Y);
}