using System.Configuration;

using DataAccess.Models;
using DataAccess.Repos;

using Microsoft.EntityFrameworkCore;

namespace ScanApp;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		var connectionString = ConfigurationManager.ConnectionStrings["RDSContext"].ConnectionString;
		var options = new DbContextOptionsBuilder<RDSContext>()
			 .UseSqlServer(connectionString)
			 .Options;
		using RDSContext context = new RDSContext(options);
		IOpisRepo opisRepo = new OpisRepo(context);
		Application.Run(new ScanForm(context, opisRepo));
	}
}