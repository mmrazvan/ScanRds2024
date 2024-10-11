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
}