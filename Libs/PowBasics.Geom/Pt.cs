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
	public double Length => Math.Sqrt(X * X + Y * Y);
}


public static class PtExt
{
	public static VecPt ToVecPt(this Pt p) => new(p.X, p.Y);

	public static int Dir(this Pt pt, Dir dir) => dir switch
	{
		Geom.Dir.Horiz => pt.X,
		Geom.Dir.Vert => pt.Y,
		_ => throw new ArgumentException()
	};

	public static Pt CapOfsLarge(this Pt ofs, Sz sz)
	{
		var x = Math.Max(0, ofs.X);
		var y = Math.Max(0, ofs.Y);
		return new Pt(
			Math.Max(0, Math.Min(x, sz.Width)),
			Math.Max(0, Math.Min(y, sz.Height))
		);
	}
}
