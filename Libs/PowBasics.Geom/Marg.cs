namespace PowBasics.Geom;

public readonly record struct Marg(int Top, int Right, int Bottom, int Left)
{
	public Marg MgUp(int top) => this with { Top = top };
	public Marg MgRight(int right) => this with { Right = right };
	public Marg MgDown(int bottom) => this with { Bottom = bottom };
	public Marg MgLeft(int left) => this with { Left = left };

	public override string ToString() => (this == default) switch
	{
		true => string.Empty,
		false when Top == Right && Top == Bottom && Top == Left => $"mg({Top})",
		_ => $"mg({Top},{Right},{Bottom},{Left})"
	};

	public static readonly Marg Empty = default;
}

public static class MargUtils
{
	public static int Dir(this Marg m, Dir dir) => dir switch
	{
		Geom.Dir.Horz => m.Left + m.Right,
		Geom.Dir.Vert => m.Top + m.Bottom,
		_ => throw new ArgumentException()
	};

	public static Marg Mirror(this Marg m) => new(m.Left, m.Bottom, m.Right, m.Top);
}
