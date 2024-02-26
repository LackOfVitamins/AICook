using AICook.Authorization.Authentication;
using AICook.Event.Configuration;
using AICook.Identity.Data;
using AICook.Identity.Services;
using AICook.Model.Profiles;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AICook.Identity;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		ConfigureServices(builder.Services, builder.Configuration);
		
		var app = builder.Build();
		
		MigrateDatabase(app);
		
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthentication();
		app.UseAuthorization();
		
		app.MapControllers();
		app.Run();
	}

	private static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
	{
		// Logging
		services.AddLogging();
		
		// Database
		var connectionString = configuration.GetConnectionString("MySqlDatabase")!;
		services.AddDbContext<IdentityContext>(
			options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
		);
		
		// Authentication & Authorization
		services.AddAuthentication()
			.AddScheme<JwtAuthenticationSchemeOptions, JwtAuthenticationSchemeHandler>(
				"JwtTokenIdentityScheme",
				_ => {}
			);
		services.AddAuthorization();
		
		// Masstransit
		var mtConfig = MassTransitConfiguration.FromConfiguration(configuration);
		services.AddMassTransit(options =>
		{
			options.AddConsumers(typeof(Program).Assembly);
            
			options.UsingRabbitMq((context, cfg) =>
			{
				cfg.Host(mtConfig.Host, mtConfig.Port, mtConfig.VirtualHost, hostCfg =>
				{
					hostCfg.Username(mtConfig.Username);
					hostCfg.Password(mtConfig.Password);
				});
                
				cfg.ConfigureEndpoints(context);
			});
		});
		
		// Controllers
		services.AddControllers();
		
		// Swagger
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		
		// Services
		services.AddScoped<IJwtService, JwtService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<ITokenService, TokenService>();
		
		// Mapper 
		services.AddAutoMapper(options =>
		{
			options.AddProfile<IdentityProfile>();
		});
	}
	
	private static void MigrateDatabase(IHost app)
	{
		using var scope = app.Services.CreateScope();
		var services = scope.ServiceProvider;

		var context = services.GetRequiredService<IdentityContext>();
		if (context.Database.GetPendingMigrations().Any())
		{
			context.Database.Migrate();
		}
	}
}
