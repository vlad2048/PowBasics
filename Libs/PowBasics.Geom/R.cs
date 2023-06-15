namespace PowBasics.Geom;

public readonly record struct R
{
	public int X { get; }
	public int Y { get; }
	public int Width { get; }
	public int Height { get; }

	public static bool UseOldBehaviorForRightAndBottom { get; set; }

	/// <summary>
	/// Represents the pixel after the right of the rectangle such that Right = X + Width
	/// </summary>
	public int Right => UseOldBehaviorForRightAndBottom switch
	{
		false => X + Width,
		true => Math.Max(X, X + Width - 1)
	};

	/// <summary>
	/// Represents the pixel after the bottom of the rectangle such that Bottom = Y + Height
	/// </summary>
	public int Bottom => UseOldBehaviorForRightAndBottom switch
	{
		false => Y + Height,
		true => Math.Max(Y, Y + Height - 1)
	};

	public Pt Pos => new(X, Y);
	public Sz Size => new(Width, Height);
	public static readonly R Empty = new(0, 0, 0, 0);
	public bool IsDegenerate => Size.IsDegenerate;

	public R(int x, int y, int width, int height)
	{
		if (width < 0 || height < 0)
			throw new ArgumentException();
		X = x;
		Y = y;
		Width = width;
		Height = height;
	}

	public R(Pt pos, Sz size) : this(pos.X, pos.Y, size.Width, size.Height)
	{
	}

	public R(Sz size) : this(0, 0, size.Width, size.Height)
	{
	}

	public static R MakeOrEmpty(int x, int y, int width, int height) => (width > 0 && height > 0) switch
	{
		true => new R(x, y, width, height),
		false => Empty
	};
	
	public override string ToString() => $"{X},{Y} {Size}";

	public static R operator +(R a, Pt b) => a == Empty ? Empty : new R(a.Pos + b, a.Size);
	public static R operator +(Pt b, R a) => a == Empty ? Empty : new R(a.Pos + b, a.Size);
	public static R operator -(R a, Pt b) => a == Empty ? Empty : new R(a.Pos - b, a.Size);
	public static R operator -(Pt b, R a) => a == Empty ? Empty : new R(a.Pos - b, a.Size);
	
	public static R operator -(R r, IMarg m)
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

	public static R operator +(R r, IMarg m) => new(r.X - m.Left, r.Y - m.Top, r.Width + m.Dir(Dir.Horz), r.Height + m.Dir(Dir.Vert));

	public Pt Center => new(X + Width / 2, Y + Height / 2);
	public static R FromCenterAndSize(Pt center, Sz size) => new(center.X - size.Width / 2, center.Y - size.Height / 2, size.Width, size.Height);

	public R Intersection(R a)
	{
		var x = Math.Max(a.X, X);
		var num1 = Math.Min(a.X + a.Width, X + Width);
		var y = Math.Max(a.Y, Y);
		var num2 = Math.Min(a.Y + a.Height, Y + Height);
        /*
            In WinDX (for Pop nodes), it's important for to have the intersection of a rectangle with
            zero size to be a rectangle with zero size at the correct location.

            This is why we have:
            num1 >= x && num2 >= y
            and not:
            num1 > x && num2 > y
        */
		return num1 >= x && num2 >= y ? new R(x, y, num1 - x, num2 - y) : R.Empty;
	}
}


public static class RExt
{
	public static VecR ToVecR(this R r) => new(new VecPt(r.X, r.Y), new VecPt(r.X + r.Width, r.Y + r.Height));

	public static int Dir(this R r, Dir dir) => dir switch
	{
		Geom.Dir.Horz => r.Width,
		Geom.Dir.Vert => r.Height,
		_ => throw new ArgumentException()
	};

	public static bool Contains(this R r, Pt pt) => pt.X >= r.X && pt.X < r.X + r.Width && pt.Y >= r.Y && pt.Y < r.Y + r.Height;

	public static bool Contains(this R a, R b) =>
		b.X >= a.X &&
		b.Y >= a.Y &&
		(b.X + b.Width) <= (a.X + a.Width) &&
		(b.Y + b.Height) <= (a.Y + a.Height);

	public static R Union(this IEnumerable<R> listE)
	{
		var list = listE.Where(e => e != R.Empty).ToList();
		if (list.Count == 0)
			return R.Empty;
		var minX = list.Min(e => e.X);
		var minY = list.Min(e => e.Y);
		var maxX = list.Max(e => e.X + e.Width);
		var maxY = list.Max(e => e.Y + e.Height);
		return new R(minX, minY, maxX - minX, maxY - minY);
	}
	
	public static R CapToMin(this R r, int minWidth, int minHeight) => new(r.X, r.Y, Math.Max(r.Width, minWidth), Math.Max(r.Height, minHeight));
	public static R WithZeroPos(this R r) => new(Pt.Empty, r.Size);
	public static R WithSize(this R r, Sz sz) => new(r.Pos, sz);
}
