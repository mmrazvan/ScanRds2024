using DataAccess.Models;
using DataAccess.Repos;

namespace ScanApp.Helpers.Scan.ScanProcessors;

public class RegularScanProcessor : IScanProcessor
{
	private readonly IOpisRepo _opisRepo;

	public RegularScanProcessor( IOpisRepo opisRepo )
	{
		_opisRepo = opisRepo;
	}

	public async Task ProcessScan( CodeData codeData )
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