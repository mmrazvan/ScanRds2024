using DataAccess.Models;

namespace ScanApp.Helpers.Scan.ScanProcessors;

public interface IScanProcessor
{
	Task ProcessScan( CodeData codeData );
}