using System.Runtime.CompilerServices;

namespace DataAccess.Helpers;

public class MethodHelpers
{
	public static string GetCallerName( [CallerMemberName] string caller = null )
	{
		return " in method: " + caller;
	}
}