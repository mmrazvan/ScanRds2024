using DataAccess.Models;
using DataAccess.Repos;

namespace ScanApp.Helpers.Scan.ScanProcessors;

public class IDScanProcessor : IScanProcessor
{
	private readonly IHeaderRepo _headerRepo;

	public IDScanProcessor( IHeaderRepo headerRepo )
	{
		_headerRepo = headerRepo;
	}

	public async Task ProcessScan( CodeData codeData )
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
			throw;
		}
	}
}