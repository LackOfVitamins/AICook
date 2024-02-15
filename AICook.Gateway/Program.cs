namespace AICook.Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("cors", corsBuilder =>
            {
	            corsBuilder.AllowAnyHeader();
	            corsBuilder.AllowAnyMethod();
	            corsBuilder.AllowAnyOrigin();
            });
        });
        
        var app = builder.Build();

        app.UseCors();
        app.MapReverseProxy();

        app.Run();
    }
}