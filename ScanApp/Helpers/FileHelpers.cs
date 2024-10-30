namespace ScanApp.Helpers;

public class FileHelpers : IFileHelpers
{
	public static void CreateFile( string folder, decimal? id )
	{
		if (id is null)
			return;
		string fileName = $"{folder}\\{id}.RP";
		if (!File.Exists(fileName))
		{
			File.Create(fileName).Close();
		}
	}

	public static void CreateFiles( string folder, int startId, int endId )
	{
		for (int i = startId; i <= endId; i++)
		{
			CreateFile(folder, i);
		}
	}
}