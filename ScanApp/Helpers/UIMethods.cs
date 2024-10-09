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
		var a = ScanHelper.CodeVerification(text);
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
				listBox.Items.Add($"       {shift.ShiftName}: {shift.ShiftProduction}");
			}
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
}