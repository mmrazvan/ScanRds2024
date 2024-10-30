using DataAccess.Models;
using DataAccess.Repos;

namespace ScanApp.Helpers.Scan.ScanProcessors;

public class BarcodeScanProcessor : IScanProcessor
{
	private readonly IHeaderRepo _headerRepo;

	public BarcodeScanProcessor( IHeaderRepo headerRepo )
	{
		_headerRepo = headerRepo;
	}

	public async Task ProcessScan( CodeData codeData )
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
}