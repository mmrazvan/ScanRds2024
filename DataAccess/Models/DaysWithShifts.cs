namespace DataAccess.Models;

public class DaysWithShifts
{
	public DateOnly Date { get; set; }
	public List<Shifts>? Shifts { get; set; }
}