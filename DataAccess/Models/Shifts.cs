namespace DataAccess.Models;

public class Shifts
{
	private readonly DateTime _date;

	public Shifts( DateTime date )
	{
		_date = date;
	}

	private readonly TimeOnly _startMorningShift = new TimeOnly(6, 0);
	private readonly TimeOnly _endMorningShift = new TimeOnly(14, 30);
	private readonly TimeOnly _startAfternoonShift = new TimeOnly(14, 30);
	private readonly TimeOnly _endAfternoonShift = new TimeOnly(23, 0);

	public DateOnly Date => DateOnly.FromDateTime(_date);
	public double ShiftProduction { get; set; }

	public double Speed { get; set; }

	public string ShiftName
	{
		get
		{
			TimeOnly currentTime = TimeOnly.FromDateTime(_date);
			if (currentTime >= _startMorningShift && currentTime < _endMorningShift)
			{
				return "Shift 1";
			}
			else if (currentTime >= _startAfternoonShift && currentTime < _endAfternoonShift)
			{
				return "Shift 2";
			}
			else
			{
				return "Shift 3";
			}
		}
	}
}