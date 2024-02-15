using Microsoft.Extensions.Configuration;

namespace AICook.Event.Configuration;

public record MassTransitConfiguration(
    string Host,
    ushort Port, 
    string VirtualHost, 
    string Username, 
    string Password)
{
    public static MassTransitConfiguration FromConfiguration(IConfigurationManager configuration)
    {
        var massTransitSection = configuration.GetSection("MassTransit");
        var massTransitHost = massTransitSection.GetValue<string>("Host")!;
        var massTransitPort = massTransitSection.GetValue<ushort>("Port", 5672);
        var massTransitVirtualHost = massTransitSection.GetValue<string>("VirtualHost", "/")!;
        var massTransitUsername = massTransitSection.GetValue<string>("Username")!;
        var massTransitPassword = massTransitSection.GetValue<string>("Password")!;

        return new MassTransitConfiguration(
            massTransitHost, 
            massTransitPort, 
            massTransitVirtualHost, 
            massTransitUsername, 
            massTransitPassword);
    }
}