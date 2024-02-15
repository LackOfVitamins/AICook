using AICook.Event.Configuration;
using AICook.Identity.Data;
using AICook.Identity.Services;
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
		
		app.MapControllers();
		app.Run();
	}

	private static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
	{
		// Logging
		services.AddLogging();
		
		var connectionString = configuration.GetConnectionString("MySqlDatabase")!;
		services.AddDbContext<IdentityContext>(
			options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
		);
		
		// services.AddIdentity<IdentityUser, IdentityRole>()
		// 	.AddEntityFrameworkStores<IdentityContext>()
		// 	.AddDefaultTokenProviders();
		//
		// services.AddAuthentication(options =>
		// 	{
		// 		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		// 		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		// 		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		// 	}
		// ).AddJwtBearer(options =>
		// 	{
		// 		options.SaveToken = true;
		// 		options.RequireHttpsMetadata = false;
		// 	}
		// );
		
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
		
		services.AddControllers();
		
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		
		services.AddScoped<IJwtService, JwtService>();
		services.AddScoped<IUserService, UserService>();
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
