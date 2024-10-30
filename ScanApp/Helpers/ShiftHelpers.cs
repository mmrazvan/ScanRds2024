using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Shifts;

namespace ScanApp.Helpers;

public static class ShiftHelpers
{
	public static List<Shifts> GetShifts( DateOnly date, List<Opis> opis )
	{
		try
		{
			List<Shifts> shifts = [];
			IEnumerable<Opis> opisFiltered = opis.Where(o => o.Data.HasValue && DateOnly.FromDateTime(o.Data.Value) == date);
			TimeSpan startScan = opisFiltered.Where(c => c.Data.HasValue).Select(c => c.Data!.Value.TimeOfDay).Min();
			TimeSpan endScan = opisFiltered.Where(c => c.Data.HasValue).Select(c => c.Data!.Value.TimeOfDay).Max();
			foreach (Opis item in opisFiltered)
			{
				if (item.Data.HasValue)
				{
					Shifts shift = new Shifts(item.Data.Value, new ShiftStrategy())
					{
						ShiftProduction = item.Cantitate ?? 0
					};
					if (!shifts.Exists(c => c.Date == shift.Date))
						shifts.Add(shift);
					else
					{
						if (!shifts.Exists(s => s.ShiftName == shift.ShiftName))
							shifts.Add(shift);
						else
						{
							Shifts existingShift = shifts.First(s => s.Date == shift.Date && s.ShiftName == shift.ShiftName);
							existingShift.ShiftProduction += shift.ShiftProduction;
						}
					}
				}
			}
			shifts[0].Speed = TimeHelpers.CalculateSpeed(startScan, endScan, shifts[0].ShiftProduction);
			return shifts;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}
}