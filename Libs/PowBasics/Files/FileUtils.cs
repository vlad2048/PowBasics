namespace PowBasics.Files;

public static class FileUtils
{
	public static string MakeTempFile(string? ext = null) => Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{ext}");
}