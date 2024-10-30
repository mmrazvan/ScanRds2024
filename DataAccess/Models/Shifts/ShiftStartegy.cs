namespace DataAccess.Models.Shifts;

public class ShiftStrategy : IShiftStrategy
{
	public string GetShiftName( DateTime currentDate )
	{
		TimeOnly currentTime = TimeOnly.FromDateTime(currentDate);
		if (currentTime >= ShiftTimes.StartMorningShift && currentTime < ShiftTimes.EndMorningShift)
		{
			return "Shift 1";
		}
		else if (currentTime >= ShiftTimes.StartAfternoonShift && currentTime < ShiftTimes.EndAfternoonShift)
		{
			return "Shift 2";
		}
		else
		{
			return "Shift 3";
		}
	}
}