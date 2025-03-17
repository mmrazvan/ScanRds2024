using System.Configuration;

using DataAccess.Models;
using DataAccess.Repos;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ScanApp;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	///
	public static IServiceProvider ServiceProvider { get; private set; }

	[STAThread]
	private static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();

		var serviceCollection = new ServiceCollection();
		ConfigureServices(serviceCollection);

		ServiceProvider = serviceCollection.BuildServiceProvider();
		Application.Run(ServiceProvider.GetRequiredService<ScanForm>());
	}

	public static void ConfigureServices( IServiceCollection services )
	{
		var connectionString = ConfigurationManager.ConnectionStrings["RDSContext"].ConnectionString;

		services.AddDbContext<RDSContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

		services.AddTransient<IOpisRepo, OpisRepo>();
		services.AddTransient<IHeaderRepo, HeaderRepo>();
		services.AddTransient<ScanForm>();
	}
}