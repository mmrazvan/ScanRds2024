using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Repos;

namespace ScanApp.Helpers;

public class UIMethods
{
	private readonly OpisRepo _opisRepo;
	private readonly HeaderRepo _headerRepo;

	public UIMethods( OpisRepo opisRepo, HeaderRepo headerRepo )
	{
		_opisRepo = opisRepo;
		_headerRepo = headerRepo;
	}

	public async Task ProcessScan( string scanText )
	{
		ScanHelper scanHelper = new ScanHelper(_opisRepo, _headerRepo);
		string text = scanText.ToUpper();
		var a = StringHelpers.CodeVerification(text);
		if (a is not null)
			await scanHelper.ProcessCodeData(a);
	}

	public async Task PopulateRemainingCountyList( ListBox listBox )
	{
		listBox.Items.Clear();
		try
		{
			List<string> remainingCountiesList = await _opisRepo.GetRemainingCountiesAsync();
			foreach (var county in remainingCountiesList)
			{
				listBox.Items.Add(county);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task PopulateBoxesCheckListbox( CheckedListBox checkedListBox, string county )
	{
		checkedListBox.Items.Clear();
		try
		{
			List<int> boxesList = await _opisRepo.GetCountyRemainingBoxesAsync(county);
			foreach (var box in boxesList)
			{
				checkedListBox.Items.Add(box);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public static void PopulateListBoxDetails( ListBox listBox, List<DaysWithShifts> daysWithShifts )
	{
		listBox.Items.Clear();
		foreach (var item in daysWithShifts)
		{
			listBox.Items.Add($"{item.Date} Total: {item.Shifts!.Sum(s => s.ShiftProduction)}");
			foreach (var shift in item.Shifts!)
			{
				listBox.Items.Add($"|       {shift.ShiftName}: {shift.ShiftProduction}");
			}
			listBox.Items.Add($"Speed: {item.Speed:.0}");
		}
	}

	public async Task UpdateProgressbarAsync( ProgressBar progressBar, Label label )
	{
		try
		{
			var totalInvoices = await _opisRepo.GetTotalInvoicesAsync();
			var remainingInvoices = await _opisRepo.GetRemainingInvoicesAsync();
			ProgressHelper.UpdateProgressbar(progressBar, totalInvoices - remainingInvoices, totalInvoices, label);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task UpdateLabelCountyDetailsAsync( Label label, string county )
	{
		try
		{
			int totalInvoices = await _opisRepo.GetTotalInvoicesByCountyAsync(county);
			int remainingInvoices = await _opisRepo.GetRemainingInvoicesByCountyAsync(county);
			var percentage = ( ( double ) ( totalInvoices - remainingInvoices ) / totalInvoices ) * 100;
			label.Text = $"Total: {totalInvoices}; Remaining: {remainingInvoices};{Environment.NewLine}{percentage:.00}% completed";
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task UpdateLabelRemainingTimeAsync( Label label )
	{
		try
		{
			DateTime finishDate = await CalculateFinishDate();
			label.Text = $"Finish date: {finishDate.DayOfWeek} {finishDate:dd.MM HH:mm}";
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
	}

	public async Task<DateTime> CalculateFinishDate()
	{
		List<DaysWithShifts> daysWithShifts = await GetDaysWithShifts();
		double averageSpeed = daysWithShifts.Average(d => d.Speed);
		int remainingProduction = await _opisRepo.GetRemainingInvoicesAsync();
		double totalRemainingHours = remainingProduction / averageSpeed;
		DateTime finishDate = DateTime.Now;
		DateOnly date = DateOnly.FromDateTime(DateTime.Now);
		while (totalRemainingHours > 0)
		{
			if (finishDate.DayOfWeek == DayOfWeek.Saturday || finishDate.DayOfWeek == DayOfWeek.Sunday)
			{
				finishDate = finishDate.AddDays(1);
				date = date.AddDays(1);
			}
			else
			{
				var workinghoursToday = TimeHelpers.GetWorkingHoursRemainingToday(date);
				var remainingHoursToday = totalRemainingHours > workinghoursToday
					? TimeHelpers.GetRemainingHours(date)
					: totalRemainingHours;
				finishDate = finishDate.AddHours(remainingHoursToday);
				totalRemainingHours -= workinghoursToday;
				date = date.AddDays(1);
			}
		}
		return finishDate;
	}

	public async Task<List<DaysWithShifts>> GetDaysWithShifts()
	{
		var daysWithShifts = new List<DaysWithShifts>();
		try
		{
			var workingDays = _opisRepo.GetWorkingDaysAsync();
			foreach (var workingDay in workingDays)
			{
				var dayWithShifts = new DaysWithShifts
				{
					Date = workingDay,
					Shifts = await _opisRepo.GetShiftsAsync(workingDay),
				};
				dayWithShifts.Speed = dayWithShifts.Shifts.Max(s => s.Speed);
				daysWithShifts.Add(dayWithShifts);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + MethodHelpers.GetCallerName(), ex.InnerException);
		}
		return daysWithShifts;
	}
}