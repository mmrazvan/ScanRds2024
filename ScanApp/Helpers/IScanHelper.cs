
using DataAccess.Models;

namespace ScanApp.Helpers;
public interface IScanHelper
{
	Task ProcessCodeData( CodeData codeData );
}