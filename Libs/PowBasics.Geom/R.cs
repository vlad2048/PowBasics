using System.Text.Json.Serialization;

namespace PowBasics.Geom;

public readonly record struct R(int X, int Y, int Width, int Height)
{
	/// <summary>
	/// Represents the pixel after the right of the rectangle such that Right = X + Width
	/// </summary>
	[JsonIgnore]
	public int Right => X + Width;

	/// <summary>
	/// Represents the pixel after the bottom of the rectangle such that Bottom = Y + Height
	/// </summary>
	[JsonIgnore]
	public int Bottom => Y + Height;

	[JsonIgnore]
	public Pt Pos => new(X, Y);
	[JsonIgnore]
	public Sz Size => new(Width, Height);
	public static readonly R Empty = new(0, 0, 0, 0);
	[JsonIgnore]
	public bool IsDegenerate => Size.IsDegenerate;

	public R(Pt pos, Sz size) : this(pos.X, pos.Y, size.Width, size.Height)
	{
	}

	public R(Sz size) : this(0, 0, size.Width, size.Height)
	{
	}

	public override string ToString() => $"{X},{Y} {Size}";

	public static R operator +(R a, Pt b) => a == Empty ? Empty : new R(a.Pos + b, a.Size);
	public static R operator +(Pt b, R a) => a == Empty ? Empty : new R(a.Pos + b, a.Size);
	public static R operator -(R a, Pt b) => a == Empty ? Empty : new R(a.Pos - b, a.Size);
	public static R operator -(Pt b, R a) => a == Empty ? Empty : new R(a.Pos - b, a.Size);

	public static R operator -(R r, Marg m)
	{
		if (m.Dir(Dir.Horz) >= r.Width || m.Dir(Dir.Vert) >= r.Height)
		{
			var pt = r.Pos + new Pt(m.Left, m.Top);
			var cappedPt = new Pt(
				Math.Min(pt.X, r.X + Math.Max(0, r.Width - 1)),
				Math.Min(pt.Y, r.Y + Math.Max(0, r.Height - 1))
			);
			return new R(cappedPt, Sz.Empty);
		}

		return new R(
			r.X + m.Left,
			r.Y + m.Top,
			r.Width - (m.Left + m.Right),
			r.Height - (m.Top + m.Bottom)
		);
	}

	public static R operator +(R r, Marg m) => new(r.X - m.Left, r.Y - m.Top, r.Width + m.Dir(Dir.Horz), r.Height + m.Dir(Dir.Vert));

	[JsonIgnore]
	public Pt Center => new(X + Width / 2, Y + Height / 2);

	public static R FromCenterAndSize(Pt center, Sz size) => new(center.X - size.Width / 2, center.Y - size.Height / 2, size.Width, size.Height);
}