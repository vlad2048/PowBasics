using Directory = System.IO.Directory;

namespace PowBasics.Files;

public static class FileExt
{
	/// <summary>
	/// Creates a folder if it doesn't exist
	/// </summary>
	/// <param name="folder">Folder to create</param>
	/// <returns>Folder given</returns>
	public static string MakeFolderIFN(this string folder)
	{
		if (!Directory.Exists(folder))
			Directory.CreateDirectory(folder);
		return folder;
	}

	/// <summary>
	/// Creates the folder corresponding to the folder component of the file given
	/// </summary>
	/// <param name="file">File</param>
	/// <returns>File given</returns>
	/// <exception cref="IOException">If the file doesn't have a folder component</exception>
	public static string MakeFolderForFileIFN(this string file)
	{
		(Path.GetDirectoryName(file) ?? throw new IOException($"Filename '{file}' doesn't have a directory component")).MakeFolderIFN();
		return file;
	}
}