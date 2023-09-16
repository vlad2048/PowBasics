using Directory = System.IO.Directory;

namespace PowBasics.Files;

public static class FileExt
{
	public static string MakeFolderIFN(this string folder)
	{
		if (!Directory.Exists(folder))
			Directory.CreateDirectory(folder);
		return folder;
	}

	public static string MakeFolderForFileIFN(this string file)
	{
		(Path.GetDirectoryName(file) ?? throw new IOException($"Filename '{file}' doesn't have a directory component")).MakeFolderIFN();
		return file;
	}
}