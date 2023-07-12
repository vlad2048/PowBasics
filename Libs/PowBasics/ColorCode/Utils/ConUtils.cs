using PowBasics.ColorCode.Structs;
using System.Drawing;

namespace PowBasics.ColorCode.Utils;

public static class ConUtils
{
	public static void Write(string str, Color c)
	{
		SetColor(c);
		Console.Write(str);
	}

	public static void WriteLine(string str, Color c)
	{
		SetColor(c);
		Console.WriteLine(str);
	}

	public static void WriteLine() => Console.WriteLine();

	public static void WriteLine(Txt txt) => PrintToConsole(txt);


	public static void PrintToConsole(this Txt txt)
	{
		foreach (var line in txt.Lines)
		{
			foreach (var chunk in line)
			{
				SetColor(chunk.Color);
				Console.Write(chunk.Text);
			}
			Console.WriteLine();
		}
	}
	
	private const char EscChar = (char)0x1B;
	private static void SetColor(Color c) => Console.Write($"{EscChar}[38;2;{c.R};{c.G};{c.B}m");
}