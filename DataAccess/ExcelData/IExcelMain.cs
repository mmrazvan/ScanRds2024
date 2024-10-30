
using DataAccess.Models;

namespace DataAccess.ExcelData;
public interface IExcelMain
{
	Task<List<Opis>> ReadExcelAsync( string path );
	Task WriteExcelAsync( string path, List<Opis> opis );
}