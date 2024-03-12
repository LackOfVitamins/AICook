using AICook.Authorization.Authentication;
using AICook.Event.Configuration;
using AICook.Model.Profiles;
using AICook.RecipeService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AICook.RecipeService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Migrating DB
        MigrateDatabase(app);

        // Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
    {
        // Logging
        services.AddLogging();
        
        // Problem 
        services.AddProblemDetails();
        
        // Model Mapping
        services.AddAutoMapper(config =>
        {
	        config.AddProfile<RecipeProfile>();
        });
        
        // Controllers
        services.AddControllersWithViews();
        
        // Database
        var connectionString = configuration.GetConnectionString("MySqlDatabase")!;
        services.AddDbContext<RecipeContext>(options => 
	        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );
            
        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Authentication & Authorization
        services.AddAuthentication()
	        .AddScheme<JwtAuthenticationSchemeOptions, JwtAuthenticationSchemeHandler>(
		        "JwtTokenIdentityScheme",
		        _ => {}
	        );
        services.AddAuthorization();
        
        // MassTransit RabbitMQ
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
    }

    private static void MigrateDatabase(IHost app)
    {
	    using var scope = app.Services.CreateScope();
	    var services = scope.ServiceProvider;

	    var context = services.GetRequiredService<RecipeContext>();
	    if (context.Database.GetPendingMigrations().Any())
	    {
		    context.Database.Migrate();
	    }
    }
}