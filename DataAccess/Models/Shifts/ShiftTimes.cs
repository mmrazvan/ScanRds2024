namespace DataAccess.Models.Shifts;

public static class ShiftTimes
{
	public static TimeOnly StartMorningShift { get; } = new TimeOnly(6, 0);
	public static TimeOnly EndMorningShift { get; } = new TimeOnly(14, 30);
	public static TimeOnly StartAfternoonShift { get; } = new TimeOnly(14, 30);
	public static TimeOnly EndAfternoonShift { get; } = new TimeOnly(23, 0);
}