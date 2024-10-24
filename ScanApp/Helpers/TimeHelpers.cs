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

	public static double GetRemainingHours( DateOnly date )
	{
		return date == DateOnly.FromDateTime(DateTime.Now) ? 24 - DateTime.Now.Hour : 24;
	}
}