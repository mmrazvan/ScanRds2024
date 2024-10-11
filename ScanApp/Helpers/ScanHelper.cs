using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Repos;

namespace ScanApp.Helpers;

public class ScanHelper
{
	private readonly OpisRepo _opisRepo;
	private readonly HeaderRepo _headerRepo;

	public ScanHelper( OpisRepo opisRepo, HeaderRepo headerRepo )
	{
		_opisRepo = opisRepo;
		_headerRepo = headerRepo;
	}

	public async Task ProcessCodeData( CodeData codeData )
	{
		try
		{
			if (!string.IsNullOrEmpty(codeData.MachineId))
			{
				//process regular scan
				await ProcessRegularScan(codeData);
			}
			else if (codeData.IdPlic != 0)
			{
				//process regular scan
				await ProcessIdScan(codeData);
			}
			else if (!string.IsNullOrEmpty(codeData.BarcodeData))
			{
				//process barcode identification
				await ProcessBarcodeScan(codeData);
			}
			else if (!string.IsNullOrEmpty(codeData.ClientCode))
			{
				//process client scan
				await ProcessClientCodeScan(codeData);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occurred during code data processing: {ex.Message}");
		}
	}

	private async Task ProcessIdScan( CodeData codeData )
	{
		try
		{
			var header = await _headerRepo.GetInvoiceByIdPlicAsync(codeData.IdPlic);
			if (header == null)
			{
				return;
			}
			if (codeData.IdPlic != 0 && codeData.IdPlicStop == 0)
				FileHelpers.CreateFile(Properties.Settings.Default.WorkFolder, header.Idplic);
			if (codeData.IdPlic != 0 && codeData.IdPlicStop != 0)
				FileHelpers.CreateFiles(Properties.Settings.Default.WorkFolder, codeData.IdPlic, codeData.IdPlicStop);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occurred during ID scan processing: {ex.Message}");
		}
	}

	private async Task ProcessClientCodeScan( CodeData codeData )
	{
		try
		{
			var header = await _headerRepo.GetInvoiceFromClientCodeAsync(codeData.ClientCode);
			if (header == null)
			{
				return;
			}
			FileHelpers.CreateFile(Properties.Settings.Default.WorkFolder, header.Idplic);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occurred during client code scan processing: {ex.Message}");
		}
	}

	private async Task ProcessBarcodeScan( CodeData codeData )
	{
		try
		{
			var header = await _headerRepo.GetInvoiceFromBarcodeAsync(codeData.BarcodeData);
			if (header == null)
			{
				return;
			}
			FileHelpers.CreateFile(Properties.Settings.Default.WorkFolder, header.Idplic);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occurred during barcode scan processing: {ex.Message}");
		}
	}

	private async Task ProcessRegularScan( CodeData codeData )
	{
		try
		{
			var opis = await _opisRepo.GetOpisByIdAsync(codeData.Cutie);
			if (opis == null || opis.Term == "x")
			{
				return;
			}
			opis.Term = codeData.Term;
			opis.Masina = codeData.MachineId;
			opis.Data = codeData.Data;
			await _opisRepo.UpdateOpisAsync(opis);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occurred during regular scan processing: {ex.Message}");
		}
	}

	public static List<Shifts> GetShifts( DateOnly date, List<Opis> opis )
	{
		try
		{
			List<Shifts> shifts = [];
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