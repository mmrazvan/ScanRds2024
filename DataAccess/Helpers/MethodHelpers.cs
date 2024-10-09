using System.Runtime.CompilerServices;

namespace DataAccess.Helpers;

public class MethodHelpers
{
	public static string GetCallerName( [CallerMemberName] string caller = null )
	{
		return " in method: " + caller;
	}

	public static double CalculateSpeed( TimeSpan startScan, TimeSpan endScan, double production )
	{
		var hours = ( endScan - startScan ).TotalHours;
		return production / hours;
	}
}