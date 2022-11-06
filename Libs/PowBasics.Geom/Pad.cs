namespace PowBasics.Geom;

public readonly record struct Pad : IMarg
{
	public int Top { get; }
	public int Right { get; }
	public int Bottom { get; }
	public int Left { get; }
	public int InBetween { get; }

	public Pad(int top, int right, int bottom, int left, int inBetween) {
		if (top < 0 || right < 0 || bottom < 0 || left < 0 || inBetween < 0) throw new ArgumentException();
		Top = top;
		Right = right;
		Bottom = bottom;
		Left = left;
		InBetween = inBetween;
	}

	public Pad PdUp(int top) => new(top, Right, Bottom, Left, InBetween);
	public Pad PdRight(int right) => new(Top, right, Bottom, Left, InBetween);
	public Pad PdDown(int bottom) => new(Top, Right, bottom, Left, InBetween);
	public Pad PdLeft(int left) => new(Top, Right, Bottom, left, InBetween);
	public Pad PdInBetween(int inBetween) => new(Top, Right, Bottom, Left, inBetween);

	public string FmtConcise() => (this == default) switch
	{
		true => string.Empty,
		false when (Top == Right && Top == Bottom && Top == Left && Top == InBetween) => $"pd({Top})",
		false when (Top == Right && Top == Bottom && Top == Left) => $"pd({Top} ;{InBetween})",
		_ => $"pd({Top},{Right},{Bottom},{Left} ;{InBetween})"
	};
}

public static class PadUtils
{
	public static Pad Mirror(this Pad m) => new(m.Left, m.Bottom, m.Right, m.Top, m.InBetween);
}