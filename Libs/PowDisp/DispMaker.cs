using System.Runtime.CompilerServices;

namespace PowDisp;

public static class DispMaker
{
	public static Disp MkD(
		string nameBase,
		[CallerFilePath] string srcFile = "",
		[CallerLineNumber] int srcLine = 0
	) => DispDiag.MkD(nameBase, srcFile, srcLine);
}