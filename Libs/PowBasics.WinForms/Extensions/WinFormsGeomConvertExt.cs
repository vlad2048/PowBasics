using PowBasics.Geom;

namespace PowBasics.WinForms.Extensions;

public static class WinFormsGeomConvertExt
{
	public static Pt ToPt(this Point p) => new(p.X, p.Y);
	public static Point ToPoint(this Pt p) => new(p.X, p.Y);

	public static Sz ToSz(this Size s) => new(s.Width, s.Height);
	public static Size ToSize(this Sz s) => new(s.Width, s.Height);

	public static R ToR(this Rectangle r) => new(r.X, r.Y, r.Width, r.Height);
	public static Rectangle ToRectangle(this R r) => new(r.X, r.Y, r.Width, r.Height);
}