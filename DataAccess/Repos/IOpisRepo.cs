
using DataAccess.Models;

namespace DataAccess.Repos;
public interface IOpisRepo
{
	Task<List<Opis>> GetAllOpisAsinc();
	Task<List<string>> GetCountiesAsync();
	Task<List<int>> GetCountyRemainingBoxesAsync( string county );
	Task<DateTime> GetLastOpisDateAsync();
	Task<List<Opis>> GetOpisAsync();
	Task<Opis?> GetOpisByIdAsync( int boxNumber );
	Task<List<string>> GetRemainingCountiesAsync();
	Task<int> GetRemainingInvoicesAsync();
	Task<int> GetRemainingInvoicesByCountyAsync( string county );
	Task<int> GetTotalInvoicesAsync();
	Task<int> GetTotalInvoicesByCountyAsync( string county );
	List<DateOnly> GetWorkingDaysAsync();
	Task UpdateOpisAsync( Opis opis );
}