﻿namespace PowBasics.StringsExt;

public static class StringExt
{
	public static string[] SplitInLines(this string? str) => str == null ? Array.Empty<string>() : str.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
	
	public static string JoinLines(this IEnumerable<string> source) => string.Join(Environment.NewLine, source);

	public static string Truncate(this string str, int lng) => str switch
	{
		null => throw new ArgumentException(),
		_ when str.Length <= lng => str,
		_ => str[..lng]
	};

	public static string TruncateOrPadRight(this string str, int lng, char padCh = ' ') => str switch
	{
		null => throw new ArgumentException(),
		_ when str.Length < lng => str.PadRight(padCh),
		_ when str.Length > lng => str[..lng],
		_ => str
	};
	
	public static string RemoveTrailing(this string str, string suffix) => str.EndsWith(suffix) ? str[..^suffix.Length] : str;
}