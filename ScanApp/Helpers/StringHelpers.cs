using DataAccess.Models;

namespace ScanApp.Helpers;

public class StringHelpers
{
	public static CodeData? CodeVerification( string code )
	{
		if (code.Length == 0)
		{
			return null;
		}
		CodeData codeData = new CodeData();
		try
		{
			switch (code.Substring(0, 1))
			{
				case "I": //regular scan
					if (code.Length > 4 & code.Length <= 8)
					{
						if (!MachineIds.MachineId.Contains(GetMachineId(code)))
						{
							return null;
						}
						codeData.MachineId = GetMachineId(code);
						codeData.Cutie = GetBoxNumber(code);
						return codeData;
					}
					else if (code.Length > 25)
					{
						//get id from barcode
						codeData.BarcodeData = GetBarcodeData(code);
						return codeData;
					}
					break;

				case "R":
				{
					if (code.Contains("-"))
					{
						codeData.IdPlic = int.Parse(code.Substring(1, code.IndexOf("-") - 1));
						codeData.IdPlicStop = int.Parse(code.Substring(code.IndexOf("-") + 1));
						return codeData;
					}
					codeData.IdPlic = int.Parse(code.Substring(1));
					return codeData;
				}

				case "C":
				{
					codeData.ClientCode = code.Substring(1);
					return codeData;
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occurred during code verification: {ex.Message}");
		}
		return null;
	}

	private static string GetBarcodeData( string code )
	{
		return code.Length < 25 ? string.Empty : code.Substring(4);
	}

	private static int GetBoxNumber( string code )
	{
		return code.Length < 5 ? 0 : int.Parse(code.Substring(4));
	}

	private static string GetMachineId( string code )
	{
		return code.Length < 4 ? string.Empty : code.Substring(0, 4);
	}
}