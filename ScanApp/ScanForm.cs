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

	public ScanForm( RDSContext context )
	{
		InitializeComponent();
		_context = context;
		_opisRepo = new OpisRepo(_context);
		_headerRepo = new HeaderRepo(_context);
		_workFolder = Settings.Default.WorkFolder;
	}

	private async void Button1_Click( object sender, EventArgs e )
	{
		ScanHelper scanHelper = new ScanHelper(_opisRepo, _headerRepo);
		string text = textBoxScan.Text.ToUpper();
		var a = ScanHelper.CodeVerification(text);
		if (a is not null)
			await scanHelper.ProcessCodeData(a);
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
}