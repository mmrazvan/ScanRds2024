using DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos;

public class OpisRepo
{
	private readonly RDSContext _context;

	public OpisRepo( RDSContext context )
	{
		_context = context;
	}

	public async Task<List<Opis>> GetOpisAsync()
	{
		return await _context.Opis.ToListAsync();
	}

	public async Task<Opis> GetOpisByIdAsync( int boxNumber )
	{
		return await _context.Opis.FirstOrDefaultAsync(o => o.NumarCutie == boxNumber);
	}

	public async Task UpdateOpisAsync( Opis opis )
	{
		try
		{
			_context.Opis.Update(opis);
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			// Handle the exception here
			// Log the error or perform any necessary actions
			throw;
		}
	}

	public async Task<int> GetTotalInvoicesAsync()
	{
		try
		{
			return await _context.Opis.SumAsync(o => o.Cantitate.HasValue ? ( int ) o.Cantitate.Value : 0);
		}
		catch (Exception ex)
		{
			// Handle the exception here
			// Log the error or perform any necessary actions
			throw;
		}
	}

	public async Task<int> GetRemainingInvoicesAsync()
	{
		try
		{
			return await _context.Opis.Where(h => h.Term != "x").SumAsync(c => c.Cantitate.HasValue ? ( int ) c.Cantitate.Value : 0);
		}
		catch (Exception ex)
		{
			// Handle the exception here
			// Log the error or perform any necessary actions
			throw;
		}
	}
}