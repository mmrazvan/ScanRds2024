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

	public static CodeData? CodeVerification( string code )
	{
		if (code.Length == 0)
		{
			return null;
		}
		CodeData codeData = new CodeData();
		switch (code.Substring(0, 1))
		{
			case "I": //regular scan
				if (code.Length > 4 & code.Length <= 8)
				{
					if (!MachineIds.MachineId.Contains(GetMachineId(code)))
					{
						return null;
					}
					codeData.MachineId = GetMachineId(code);
					codeData.Cutie = GetBoxNumber(code);
					return codeData;
				}
				else if (code.Length > 25)
				{
					//get id from barcode
					codeData.BarcodeData = GetBarcodeData(code);
					return codeData;
				}
				break;

			case "R":
			{
				if (code.Contains("-"))
				{
					codeData.IdPlic = int.Parse(code.Substring(1, code.IndexOf("-") - 1));
					codeData.IdPlicStop = int.Parse(code.Substring(code.IndexOf("-") + 1));
					return codeData;
				}
				codeData.IdPlic = int.Parse(code.Substring(1));
				return codeData;
			}

			case "C":
			{
				codeData.ClientCode = code.Substring(1);
				return codeData;
			}
		}
		return null;
	}

	private static string GetMachineId( string code )
	{
		return code.Length < 4 ? string.Empty : code.Substring(0, 4);
	}

	private static int GetBoxNumber( string code )
	{
		return code.Length < 5 ? 0 : int.Parse(code.Substring(4));
	}

	private static string GetBarcodeData( string code )
	{
		return code.Length < 25 ? string.Empty : code.Substring(4);
	}

	public async Task ProcessCodeData( CodeData codeData )
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

	private async Task ProcessIdScan( CodeData codeData )
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

	private async Task ProcessClientCodeScan( CodeData codeData )
	{
		var header = await _headerRepo.GetInvoiceFromClientCodeAsync(codeData.ClientCode);
		if (header == null)
		{
			return;
		}
		FileHelpers.CreateFile(Properties.Settings.Default.WorkFolder, header.Idplic);
	}

	private async Task ProcessBarcodeScan( CodeData codeData )
	{
		var header = await _headerRepo.GetInvoiceFromBarcodeAsync(codeData.BarcodeData);
		if (header == null)
		{
			return;
		}
		FileHelpers.CreateFile(Properties.Settings.Default.WorkFolder, header.Idplic);
	}

	private async Task ProcessRegularScan( CodeData codeData )
	{
		var opis = await _opisRepo.GetOpisByIdAsync(codeData.Cutie);
		if (opis == null)
		{
			return;
		}
		opis.Term = codeData.Term;
		opis.Masina = codeData.MachineId;
		opis.Data = codeData.Data;
		await _opisRepo.UpdateOpisAsync(opis);
	}
}