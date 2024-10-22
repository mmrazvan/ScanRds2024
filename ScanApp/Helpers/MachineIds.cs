namespace ScanApp.Helpers;

public static class MachineIds
{
	private static readonly List<string> _machineId =
		  [
				"ID00","ID01","ID02","ID03","ID04","ID05","ID06","ID07","ID08","ID09","ID10"
		  ];

	public static IReadOnlyList<string> MachineId => _machineId.AsReadOnly();
}