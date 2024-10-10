namespace ScanApp.Helpers;

public class TimeHelpers
{
	public static double GetWorkingHoursRemainingToday( DateOnly date )
	{
		return date == DateOnly.FromDateTime(DateTime.Now)
			? 22 - DateTime.Now.Hour
			: date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ? 0 : 16;
	}

	public static double GetRemainingHours( DateOnly date )
	{
		return date == DateOnly.FromDateTime(DateTime.Now) ? 24 - DateTime.Now.Hour : 24;
	}
}