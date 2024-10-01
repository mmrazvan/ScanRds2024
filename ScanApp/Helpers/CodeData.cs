namespace ScanApp.Helpers;

public class CodeData
{
	public string MachineId { get; set; } = string.Empty;
	public int Cutie { get; set; } = 0;
	public string Term { get; set; } = "x";
	public DateTime Data { get; set; } = DateTime.Now;
	public int IdPlic { get; set; } = 0;
	public int IdPlicStop { get; set; } = 0;
	public string ClientCode { get; set; } = string.Empty;
	public string BarcodeData { get; set; } = string.Empty;
}