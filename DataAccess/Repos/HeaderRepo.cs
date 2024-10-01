using DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos;
public class HeaderRepo
{
	private readonly RDSContext _context;

	public HeaderRepo( RDSContext context )
	{
		_context = context;
	}

	public RDSContext Context => _context;

	public async Task<List<HeaderE>> GetHeaderAsync()
	{
		return await Context.HeaderE.ToListAsync();
	}

	public async Task<HeaderE?> GetInvoiceByIdPlicAsync( decimal idPlic )
	{
		return await Context.HeaderE.FirstOrDefaultAsync(h => h.Idplic == idPlic);
	}

	public async Task<HeaderE?> GetInvoiceFromBarcodeAsync( string barcode )
	{
		return await Context.HeaderE.FirstOrDefaultAsync(h => h.F18 == barcode);
	}

	public async Task<HeaderE?> GetInvoiceFromClientCodeAsync( string clientCode )
	{
		return await Context.HeaderE.FirstOrDefaultAsync(h => h.F7 == clientCode);
	}
}
