using AICook.Event.Configuration;
using MassTransit;
using OpenAI_API;

namespace AICook.AIWorkerService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddEnvironmentVariables();
        
        ConfigureServices(builder.Services, builder.Configuration);
        
        var host = builder.Build();
        host.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
    {
        // Logging
        services.AddLogging();
        
        // OpenAI
        services.AddScoped<IOpenAIAPI>(_ => new OpenAIAPI(configuration.GetSection("OpenAI").GetValue<string>("ApiKey")));
        
        // S3 Storage Bucket
        services.AddMemoryCache();
        services.AddBackblazeAgent(configuration.GetSection("BackblazeBucket"));
        
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
}