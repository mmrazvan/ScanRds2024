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
			return currentTime >= _startMorningShift && currentTime < _endMorningShift
				? "Shift 1"
				: currentTime >= _startAfternoonShift && currentTime < _endAfternoonShift
				? "Shift 2" : "Shift 3";
		}
	}
}