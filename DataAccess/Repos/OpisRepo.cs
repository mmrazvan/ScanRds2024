using DataAccess.Helpers;
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
		try
		{
			_context.Opis.Update(opis);
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task<int> GetTotalInvoicesAsync()
	{
		try
		{
			int a = await _context.Opis.SumAsync(o => o.Cantitate.HasValue ? ( int ) o.Cantitate.Value : 0);
			return a;
		}
		catch (Microsoft.Data.SqlClient.SqlException ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
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
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task<List<int>> GetCountyRemainingBoxesAsync( string county )
	{
		try
		{
			return await _context.Opis.Where(h => h.Judet == county && h.Term != "x").Select(c => c.NumarCutie).ToListAsync();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task<List<string>> GetCountiesAsync()
	{
		try
		{
			return await _context.Opis.Select(c => c.Judet).Distinct().ToListAsync();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	private async Task<bool> AllCountyBoxesMarked( string county )
	{
		var a = await _context.Opis.Where(h => h.Judet == county).Where(h => h.Term != "x").SumAsync(c => c.Cantitate);
		return a == 0;
	}

	public async Task<List<string>> GetRemainingCountiesAsync()
	{
		try
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
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public List<DateOnly> GetWorkingDaysAsync()
	{
		try
		{
			var workingDays = _context.Opis.ToList()
				.Select(o => o.Data)
				.Where(o => o.HasValue)
				.Select(data => DateOnly.FromDateTime(data.Value)).OrderDescending().ToList();
			var a = workingDays
				.Distinct().ToList();
			return a;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task<int> GetTotalInvoicesByCountyAsync( string county )
	{
		try
		{
			return await _context.Opis.Where(c => c.Judet == county).SumAsync(c => c.Cantitate.HasValue ? ( int ) c.Cantitate.Value : 0);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task<int> GetRemainingInvoicesByCountyAsync( string county )
	{
		try
		{
			return await _context.Opis.Where(c => c.Judet == county && c.Term != "x").SumAsync(c => c.Cantitate.HasValue ? ( int ) c.Cantitate.Value : 0);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task<List<Shifts>> GetShiftsAsync( DateOnly date )
	{
		try
		{
			List<Shifts> shifts = [];
			List<Opis> opis = await _context.Opis.ToListAsync();
			IEnumerable<Opis> opisFiltered = opis.Where(o => o.Data.HasValue && DateOnly.FromDateTime(o.Data.Value) == date);
			var startScan = opisFiltered.Where(c => c.Data.HasValue).Select(c => c.Data.Value.TimeOfDay).Min();
			var endScan = opisFiltered.Where(c => c.Data.HasValue).Select(c => c.Data.Value.TimeOfDay).Max();
			foreach (Opis item in opisFiltered)
			{
				if (item.Data.HasValue)
				{
					Shifts shift = new Shifts(item.Data.Value)
					{
						ShiftProduction = item.Cantitate ?? 0
					};
					if (!shifts.Any(c => c.Date == shift.Date))
						shifts.Add(shift);
					else
					{
						if (!shifts.Any(s => s.ShiftName == shift.ShiftName))
							shifts.Add(shift);
						else
						{
							Shifts existingShift = shifts.First(s => s.Date == shift.Date && s.ShiftName == shift.ShiftName);
							existingShift.ShiftProduction += shift.ShiftProduction;
						}
					}
				}
			}
			shifts[0].Speed = MethodHelpers.CalculateSpeed(startScan, endScan, shifts[0].ShiftProduction);
			return shifts;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}
}