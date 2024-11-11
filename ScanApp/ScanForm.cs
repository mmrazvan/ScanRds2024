using DataAccess.Models;
using DataAccess.Repos;

using ScanApp.Helpers;
using ScanApp.Properties;

namespace ScanApp;

public partial class ScanForm : Form
{
	private readonly RDSContext _context;
	private readonly IOpisRepo _opisRepo;
	private readonly IHeaderRepo _headerRepo;
	private string _workFolder;
	private readonly UIMethods _uiMethods;

	public ScanForm( RDSContext context, IOpisRepo opisRepo )
	{
		InitializeComponent();
		_context = context;
		_opisRepo = opisRepo;
		_headerRepo = new HeaderRepo(_context);
		_workFolder = Settings.Default.WorkFolder;
		_uiMethods = new UIMethods(_opisRepo, _headerRepo);
	}

	private async void Button1_Click( object sender, EventArgs e )
	{
		foreach (var item in checkedListBox1.CheckedItems)
		{
			var opis = await _opisRepo.GetOpisByIdAsync(( int ) item);
			opis!.Term = "x";
			opis.Data = DateTime.Now;
			await _opisRepo.UpdateOpisAsync(opis);
		}
		checkedListBox1.Items.Clear();
		try
		{
			UIMethods.PopulateRemainingCountyList(listBoxCounty, await _opisRepo.GetRemainingCountiesAsync());
		}
		catch (Exception ex)
		{
			labelStatus.Text = ex.Message;
		}
		UIMethods.PopulateListBoxDetails(listBoxDetails, await _uiMethods.GetDaysWithShifts());
		textBoxScan.Focus();
	}

	private async void ScanForm_Load( object sender, EventArgs e )
	{
		try
		{
			await _uiMethods.UpdateProgressbarAsync(progressBarScan, labelProgress);
			UIMethods.PopulateRemainingCountyList(listBoxCounty, await _opisRepo.GetRemainingCountiesAsync());
			UIMethods.PopulateListBoxDetails(listBoxDetails, await _uiMethods.GetDaysWithShifts());
			await _uiMethods.UpdateLabelRemainingTimeAsync(labelStatus);
		}
		catch (Exception ex)
		{
			labelStatus.Text = ex.Message;
		}
		textBoxScan.Focus();
	}

	private void ButtonSelectWorkfolder_Click( object sender, EventArgs e )
	{
		FolderBrowserDialog fbd = new FolderBrowserDialog();
		if (_workFolder != "")
			fbd.SelectedPath = _workFolder;
		if (fbd.ShowDialog() == DialogResult.OK)
		{
			_workFolder = fbd.SelectedPath;
			Settings.Default.WorkFolder = _workFolder;
			Settings.Default.Save();
		}
	}

	private async void TextBoxScan_KeyDown( object sender, KeyEventArgs e )
	{
		if (e.KeyCode == Keys.Enter)
		{
			try
			{
				await _uiMethods.ProcessScan(textBoxScan.Text);
				await _uiMethods.UpdateProgressbarAsync(progressBarScan, labelProgress);
				UIMethods.PopulateRemainingCountyList(listBoxCounty, await _opisRepo.GetRemainingCountiesAsync());
				UIMethods.PopulateListBoxDetails(listBoxDetails, await _uiMethods.GetDaysWithShifts());
				textBoxScan.Clear();
				textBoxScan.Focus();
			}
			catch (Exception ex)
			{
				labelStatus.Text = ex.Message;
			}
		}
	}

	private async void ListBoxCounty_SelectedValueChanged( object sender, EventArgs e )
	{
		if (listBoxCounty.SelectedItem is not null)
		{
			try
			{
				await _uiMethods.PopulateBoxesCheckListbox(checkedListBox1, listBoxCounty.SelectedItem.ToString()!);
				await _uiMethods.UpdateLabelCountyDetailsAsync(labelCountyDetails, listBoxCounty.SelectedItem.ToString()!);
			}
			catch (Exception ex)
			{
				labelStatus.Text = ex.Message;
			}
		}
	}

	private async void Button2_Click( object sender, EventArgs e )
	{
		// This method is intentionally left empty.
		// Future implementation can be added here if needed.
		await Task.CompletedTask;
	}

	private void ButtonSelectAll_Click( object sender, EventArgs e )
	{
		for (int i = 0; i < checkedListBox1.Items.Count; i++)
		{
			checkedListBox1.SetItemChecked(i, true);
		}
	}

	private async void TextBoxScan_Leave( object sender, EventArgs e )
	{
		labelActions.ForeColor = Color.Red;
		labelActions.Text = "Wait!";
		await Task.Delay(TimeSpan.FromMinutes(1));
		textBoxScan.Focus();
	}

	private void TextBoxScan_Enter( object sender, EventArgs e )
	{
		labelActions.ForeColor = Color.Green;
		labelActions.Text = "Scan";
	}
}