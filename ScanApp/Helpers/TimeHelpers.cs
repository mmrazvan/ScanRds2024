namespace ScanApp.Helpers;

public static class TimeHelpers
{
	public static double GetWorkingHoursRemainingToday( DateOnly date )
	{
		if (date == DateOnly.FromDateTime(DateTime.Now))
		{
			return 22 - DateTime.Now.Hour;
		}
		else
		{
			return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday ? 0 : 16;
		}
	}

	public static double CalculateSpeed( TimeSpan startScan, TimeSpan endScan, double production )
	{
		const double epsilon = 1e-10;
		var hours = ( endScan - startScan ).TotalHours;
		return Math.Abs(hours) < epsilon ? 0 : production / hours;
	}

	public static double GetRemainingHours( DateOnly date )
	{
		return date == DateOnly.FromDateTime(DateTime.Now) ? 24 - DateTime.Now.Hour : 24;
	}
}