namespace PowBasics.Geom;


public enum Dir
{
	Horiz,
	Vert
}

public static class DirUtils
{
	public static Dir Neg(this Dir dir) => dir switch {
		Dir.Horiz => Dir.Vert,
		Dir.Vert => Dir.Horiz,
		_ => throw new ArgumentException()
	};
}
