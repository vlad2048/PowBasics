using System.Drawing;

namespace PowBasics.ColorCode.Structs;

public sealed record Txt(
	TxtChunk[][] Lines
)
{
	public static Txt FromChunk(string text, Color color) => new(new[]
	{
		new[] { new TxtChunk(text, color) }
	});
}


public static class TxtExt
{
	public static (int, int) GetSize(this Txt txt) => txt.Lines.All(e => e.GetLng() == 0) switch
	{
		true => (0, 0),
		false => (
			txt.Lines.Max(e => e.GetLng()),
			txt.Lines.Length
		)
	};

	private static int GetLng(this IEnumerable<TxtChunk> line) => line.Sum(e => e.Text.Length);
}