namespace ScanApp.Helpers;

public interface IFileHelpers
{
	static abstract void CreateFile( string folder, decimal? id );

	static abstract void CreateFiles( string folder, int startId, int endId );
}