using DataAccess.Models;

namespace ScanApp.Helpers;

public interface IScanHelper
{
	Task ProcessBarcodeScan( CodeData codeData );

	Task ProcessClientCodeScan( CodeData codeData );

	Task ProcessCodeData( CodeData codeData );

	Task ProcessIdScan( CodeData codeData );

	Task ProcessRegularScan( CodeData codeData );
}