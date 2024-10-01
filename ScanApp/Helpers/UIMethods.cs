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
}