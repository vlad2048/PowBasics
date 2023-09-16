namespace PowBasics.Files;

public static class FileUtils
{
	/// <summary>
	/// Creates a temporary filename that is guaranteed not to exist
	/// </summary>
	/// <param name="ext">Desired extension or null for no extension</param>
	/// <returns>Temporary filename</returns>
	public static string MakeTempFile(string? ext = null) => Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{ext}");
}