namespace DataAccess.Models.Shifts;

public class Shifts
{
	private readonly DateTime _date;
	private readonly IShiftStrategy _shiftStrategy;

	public Shifts( DateTime date, IShiftStrategy shiftStrategy )
	{
		_date = date;
		_shiftStrategy = shiftStrategy;
	}

	public DateOnly Date => DateOnly.FromDateTime(_date);
	public double ShiftProduction { get; set; }

	public double Speed { get; set; }

	public string ShiftName
	{
		get
		{
			return _shiftStrategy.GetShiftName(_date);
		}
	}
}