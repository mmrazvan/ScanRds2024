using DataAccess.Models;
using DataAccess.Repos;

namespace ScanApp.Helpers.Scan.ScanProcessors;

public class ClientCodeScanProcessor : IScanProcessor
{
	private readonly IHeaderRepo _headerRepo;

	public ClientCodeScanProcessor( IHeaderRepo headerRepo )
	{
		_headerRepo = headerRepo;
	}

	public async Task ProcessScan( CodeData codeData )
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
}