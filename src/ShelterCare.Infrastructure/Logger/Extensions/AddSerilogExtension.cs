using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ShelterCare.Infrastructure.Logger.Extensions;

public static class AddSerilogExtension
{
    public static void AddWebHostSerilog(this ILoggingBuilder loggingBuilder,IConfiguration configuration)
    {
        string seqServerUrl = configuration.GetSection("SeqOptions:ServerUrl").Value ?? string.Empty;
        Serilog.ILogger logger = new LoggerConfiguration()
                .WriteTo
                .Seq(
                  seqServerUrl,
                  Serilog.Events.LogEventLevel.Information)
                .CreateLogger();
        loggingBuilder.AddSerilog(logger);
        Log.Logger = logger;
    }

    public static void AddHostSerilog(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        string seqServerUrl = configuration.GetSection("SeqOptions:ServerUrl").Value ?? string.Empty;
        hostBuilder.UseSerilog();
        Serilog.ILogger logger = new LoggerConfiguration()
                .WriteTo
                .Seq(
                  seqServerUrl,
                  Serilog.Events.LogEventLevel.Information)
                .CreateLogger();
        Log.Logger = logger;
    }
}
