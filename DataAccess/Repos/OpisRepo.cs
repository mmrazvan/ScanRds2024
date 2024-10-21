using DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos;

public class OpisRepo( RDSContext context ) : IOpisRepo
{
	private readonly RDSContext _context = context;

	public async Task<List<Opis>> GetOpisAsync()
	{
		return await _context.Opis.ToListAsync();
	}

	public async Task<List<Opis>> GetAllOpisAsinc()
	{
		return await _context.Opis.ToListAsync();
	}

	public async Task<Opis?> GetOpisByIdAsync( int boxNumber )
	{
		return await _context.Opis.FirstOrDefaultAsync(o => o.NumarCutie == boxNumber);
	}

	public async Task UpdateOpisAsync( Opis opis )
	{
		_context.Opis.Update(opis);
		await _context.SaveChangesAsync();
	}

	public async Task<int> GetTotalInvoicesAsync()
	{
		int a = await _context.Opis.SumAsync(o => o.Cantitate.HasValue ? ( int ) o.Cantitate.Value : 0);
		return a;
	}

	public async Task<int> GetRemainingInvoicesAsync()
	{
		return await _context.Opis.Where(h => h.Term != "x").SumAsync(c => c.Cantitate.HasValue ? ( int ) c.Cantitate.Value : 0);
	}

	public async Task<List<int>> GetCountyRemainingBoxesAsync( string county )
	{
		return await _context.Opis.Where(h => h.Judet == county && h.Term != "x").Select(c => c.NumarCutie).ToListAsync();
	}

	public async Task<List<string>> GetCountiesAsync()
	{
		return await _context.Opis.Select(c => c.Judet).Distinct().ToListAsync();
	}

	private async Task<bool> AllCountyBoxesMarked( string county )
	{
		var a = await _context.Opis.Where(h => h.Judet == county).Where(h => h.Term != "x").SumAsync(c => c.Cantitate);
		return Math.Abs(a ?? 0) < 0.0001;
	}

	public async Task<List<string>> GetRemainingCountiesAsync()
	{
		var counties = await _context.Opis.ToListAsync();
		var notCompletedCounties = counties
			.GroupBy(c => c.Judet)
			.Where(c => !c.All(h => h.Term != "x" && h.Masina != ""))
			.Select(c => c.Key).Order()
			.ToList();
		var a = new List<string>(notCompletedCounties);
		foreach (string? county in notCompletedCounties)
		{
			if (await AllCountyBoxesMarked(county))
				a.Remove(county);
		}
		return a;
	}

	public List<DateOnly> GetWorkingDaysAsync()
	{
		var workingDays = _context.Opis.AsEnumerable()
			 .Select(o => o.Data)
			 .Where(o => o.HasValue)
			 .Select(data => data.HasValue ? DateOnly.FromDateTime(data.Value) : default).OrderDescending().ToList();
		var a = workingDays
			 .Distinct().ToList();
		return a;
	}

	public async Task<int> GetTotalInvoicesByCountyAsync( string county )
	{
		return await _context.Opis.Where(c => c.Judet == county).SumAsync(c => c.Cantitate.HasValue ? ( int ) c.Cantitate.Value : 0);
	}

	public async Task<int> GetRemainingInvoicesByCountyAsync( string county )
	{
		return await _context.Opis.Where(c => c.Judet == county && c.Term != "x").SumAsync(c => c.Cantitate.HasValue ? ( int ) c.Cantitate.Value : 0);
	}

	public Task<DateTime> GetLastOpisDateAsync()
	{
		var lastOpis = _context.Opis.OrderByDescending(o => o.Data).FirstOrDefault();
		return Task.FromResult(lastOpis?.Data ?? DateTime.Now);
	}
}