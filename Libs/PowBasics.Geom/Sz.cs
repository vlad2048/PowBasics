namespace PowBasics.Geom;


public readonly record struct Sz
{
	public int Width { get; }
	public int Height { get; }
	public bool IsDegenerate => Width == 0 || Height == 0;
	public static readonly Sz Empty = new(0, 0);

	public Sz(int width, int height)
	{
		if (width < 0 || height < 0) throw new ArgumentException();
		Width = width;
		Height = height;
	}

	public override string ToString() => $"{Width}x{Height}";

	public static Sz operator -(Sz a, Sz b) => new(Math.Max(0, a.Width - b.Width), Math.Max(0, a.Height - b.Height));
	public static Sz operator *(Sz a, int z) => new(a.Width * z, a.Height * z);
	public static Sz operator *(Sz a, double z) => new((int)(a.Width * z), (int)(a.Height * z));
}


public static class SzExt
{
	public static VecPt ToVecPt(this Sz s) => new(s.Width, s.Height);

	public static R ToR(this Sz s) => new(Pt.Empty, s);

	public static int Dir(this Sz size, Dir dir) => dir switch
	{
		Geom.Dir.Horz => size.Width,
		Geom.Dir.Vert => size.Height,
		_ => throw new ArgumentException()
	};

	public static Sz FlipIfVert(this Sz size, Dir dir) => dir switch
	{
		Geom.Dir.Horz => size,
		Geom.Dir.Vert => new Sz(size.Height, size.Width),
		_ => throw new ArgumentException()
	};
}
