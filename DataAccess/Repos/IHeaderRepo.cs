
using DataAccess.Models;

namespace DataAccess.Repos;
public interface IHeaderRepo
{
	RDSContext Context { get; }

	Task<List<HeaderE>> GetHeaderAsync();
	Task<HeaderE?> GetInvoiceByIdPlicAsync( decimal idPlic );
	Task<HeaderE?> GetInvoiceFromBarcodeAsync( string barcode );
	Task<HeaderE?> GetInvoiceFromClientCodeAsync( string clientCode );
}