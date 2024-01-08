using AICook.Event.Configuration;
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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        // app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
    {
        // Logging
        services.AddLogging();
        
        // Mapping & controllers
        services.AddAutoMapper(typeof(Program));
        services.AddControllersWithViews();
        
        // Database
        var connectionString = configuration.GetConnectionString("MySqlDatabase")!;
        services.AddDbContext<RecipeContext>(options => 
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
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

    private static void MigrateDatabase(WebApplication app) 
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<RecipeContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}