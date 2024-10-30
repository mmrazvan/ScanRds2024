using DataAccess.Models;

using ScanApp.Helpers.Scan.ScanProcessors;

namespace ScanApp.Helpers.Scan;

public class ScanHelper( IEnumerable<IScanProcessor> scanProcessors ) : IScanHelper
{
	private readonly Dictionary<string, IScanProcessor> _scanProcessors = scanProcessors.ToDictionary(s => s.GetType().Name);

	public async Task ProcessCodeData( CodeData codeData )
	{
		try
		{
			if (!string.IsNullOrEmpty(codeData.MachineId))
			{
				//process regular scan
				await _scanProcessors[nameof(RegularScanProcessor)].ProcessScan(codeData);
			}
			else if (codeData.IdPlic != 0)
			{
				//process id scan
				await _scanProcessors[nameof(IDScanProcessor)].ProcessScan(codeData);
			}
			else if (!string.IsNullOrEmpty(codeData.BarcodeData))
			{
				//process barcode identification
				await _scanProcessors[nameof(BarcodeScanProcessor)].ProcessScan(codeData);
			}
			else if (!string.IsNullOrEmpty(codeData.ClientCode))
			{
				//process client scan
				await _scanProcessors[nameof(ClientCodeScanProcessor)].ProcessScan(codeData);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occurred during code data processing: {ex.Message}");
			throw;
		}
	}
}