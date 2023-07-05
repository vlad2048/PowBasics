using System.Drawing;
using System.Reflection;
using System.Text;
using PowBasics.ColorCode.Structs;

namespace PowBasics.ColorCode.Utils;

public static class HtmlRenderer
{
	/// <summary>
	/// Render the colored Txt to an .html file
	/// </summary>
	/// <param name="txt">Txt to render</param>
	/// <param name="filename">.html filename</param>
	/// <param name="colorClass">If specified, the 'static readonly Color' fields of that type will be used as CSS variables in the output</param>
	public static void RenderToHtml(this Txt txt, string filename, Type? colorClass = null)
	{
		var colorMap = colorClass.GetColorMap();

		var sb = new StringBuilder();

		sb.AppendLine(
			"""
			<!DOCTYPE html>
			<html>

				<head>
					<style>
						body {
							background-color: #0C0C0C;
							color: white;
							font-family: 'Cascadia Mono';
							font-size: 12pt;
						}
						.maindiv {
							position: relative;
						}
						span {
							position: absolute;
						}
						:root {
			"""
		);

		foreach (var (col, name) in colorMap)
		{
			sb.AppendLine($"				--{name}: {col.ToHex()};");
		}

		sb.AppendLine(
			"""
						}
					</style>
				</head>

				<body>
					<div>
			"""
		);

		var y = 0.0;
		foreach (var line in txt.Lines)
		{
			sb.AppendLine(
			"""
						<div>
			"""
			);
			var x = 0;
			foreach (var chunk in line)
			{
				var (text, color) = chunk;
				var colorStr = colorMap.TryGetValue(color, out var name) switch
				{
					true => $"var(--{name})",
					false => color.ToHex()
				};
				var xStr = $"{x}ch";
				var yStr = $"{y:F2}em";
				sb.AppendLine(
			$"""
							<span style="left:{xStr}; top:{yStr}; color:{colorStr}">{text}</span>
			"""
				);
				x += text.Length;
			}
			sb.AppendLine(
			"""
						</div>
			"""
			);
			y += 1.19;
		}

		sb.AppendLine(
			"""
					</div>
				</body>
			</html>
			"""
		);

		File.WriteAllText(filename, sb.ToString());
	}


	private static string ToHex(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

	private static IReadOnlyDictionary<Color, string> GetColorMap(this Type? type) =>
		type switch
		{
			null =>
				new Dictionary<Color, string>(),
			not null =>
				type.GetFields(BindingFlags.Static | BindingFlags.Public)
					.Where(e => e.FieldType == typeof(Color))
					.ToDictionarySafe(
						e => (Color)e.GetValue(null)!,
						e => e.Name
					)
		};

	private static Dictionary<K, V> ToDictionarySafe<T, K, V>(this IEnumerable<T> source, Func<T, K> keyFun, Func<T, V> valFun) where K : notnull
	{
		var dict = new Dictionary<K, V>();
		foreach (var elt in source)
		{
			var (key, val) = (keyFun(elt), valFun(elt));
			dict.TryAdd(key, val);
		}
		return dict;
	}
}