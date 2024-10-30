namespace DataAccess.Models.Shifts;

public class DaysWithShifts
{
	public DateOnly Date { get; set; }
	public List<Shifts>? Shifts { get; set; }
	public double Speed { get; set; }
}