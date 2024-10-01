using DataAccess.Models;
using DataAccess.Repos;

using ScanApp.Helpers;
using ScanApp.Properties;

namespace ScanApp;

public partial class ScanForm : Form
{
	private readonly RDSContext _context;
	private readonly OpisRepo _opisRepo;
	private readonly HeaderRepo _headerRepo;
	private string _workFolder = string.Empty;
	private readonly UIMethods _uiMethods;

	public ScanForm( RDSContext context )
	{
		InitializeComponent();
		_context = context;
		_opisRepo = new OpisRepo(_context);
		_headerRepo = new HeaderRepo(_context);
		_workFolder = Settings.Default.WorkFolder;
		_uiMethods = new UIMethods(_opisRepo, _headerRepo);
	}

	private async void Button1_Click( object sender, EventArgs e )
	{
		List<string> a = await _opisRepo.GetRemainingCountiesAsync();
		List<CountyList> b = [];
		foreach (var item in a)
		{
			CountyList county = new CountyList
			{
				CountyName = item,
				RemainingBoxes = await _opisRepo.GetCountyRemainingBoxes(item)
			};
			b.Add(county);
		}
	}

	private async void ScanForm_Load( object sender, EventArgs e )
	{
		var totalInvoices = await _opisRepo.GetTotalInvoicesAsync();
		var remainingInvoices = await _opisRepo.GetRemainingInvoicesAsync();
		ProgressHelper.UpdateProgressbar(progressBarScan, totalInvoices - remainingInvoices, totalInvoices, labelProgress);
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
			await _uiMethods.ProcessScan(textBoxScan.Text);
			textBoxScan.Clear();
			textBoxScan.Focus();
		}
	}
}