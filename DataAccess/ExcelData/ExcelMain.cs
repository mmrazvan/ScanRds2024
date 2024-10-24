using DataAccess.Models;

using MiniExcelLibs;

namespace DataAccess.ExcelData;

public static class ExcelMain
{
	public static async Task WriteExcelAsync( string path, List<Opis> opis )
	{
		using FileStream stream = File.Create(path);
		await stream.SaveAsAsync(opis);
	}

	public static async Task<List<Opis>> ReadExcelAsync( string path )
	{
		IEnumerable<Opis> result = await MiniExcel.QueryAsync<Opis>(path);
		return result.ToList();
	}
}