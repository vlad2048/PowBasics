using System.Drawing;
using PowBasics.ColorCode;
using PowBasics.ColorCode.Utils;

namespace Playground;

static class Program
{
	static void Main()
	{
		var w = new TxtWriter();
		w.WriteLine("First", c);
		w.Push();
		w.WriteLine("Second", c);
		w.Push();
		w.WriteLine("Third", c);
		w.Pop();
		w.WriteLine("Fourth", c);
		w.Pop();
		w.WriteLine("Second", c);

		w.Txt.PrintToConsole();
	}

	private static readonly Color c = Color.White;
}
