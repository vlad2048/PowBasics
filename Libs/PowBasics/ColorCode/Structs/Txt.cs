using System.Drawing;

namespace PowBasics.ColorCode.Structs;

public sealed record Txt(
	TxtChunk[][] Lines
)
{
	internal static Txt FromChunk(string text, Color color) => new(new[]
	{
		new[] { new TxtChunk(text, color) }
	});
}