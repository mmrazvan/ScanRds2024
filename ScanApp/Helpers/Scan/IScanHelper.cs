using DataAccess.Models;

namespace ScanApp.Helpers.Scan;

public interface IScanHelper
{
	Task ProcessCodeData( CodeData codeData );
}