using DataAccess.Models;

using MiniExcelLibs;

namespace DataAccess.ExcelData;

public class ExcelMain : IExcelMain
{
	public async Task WriteExcelAsync( string path, List<Opis> opis )
	{
		using FileStream stream = File.Create(path);
		await stream.SaveAsAsync(opis);
	}

	public async Task<List<Opis>> ReadExcelAsync( string path )
	{
		IEnumerable<Opis> result = await MiniExcel.QueryAsync<Opis>(path);
		return result.ToList();
	}
}