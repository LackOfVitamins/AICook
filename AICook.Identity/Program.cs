namespace AICook.Identity;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		ConfigureServices(builder.Services, builder.Configuration);
		
		var app = builder.Build();
		app.Run();
	}

	private static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
	{
		// Logging
		services.AddLogging();
		
	}
}
