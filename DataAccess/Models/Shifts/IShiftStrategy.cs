namespace DataAccess.Models.Shifts;

public interface IShiftStrategy
{
	string GetShiftName( DateTime currentDate );
}