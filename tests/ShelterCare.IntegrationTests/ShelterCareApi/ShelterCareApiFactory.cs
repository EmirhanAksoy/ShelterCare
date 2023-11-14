

using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using ShelterCare.API.Marker;
using System.Data;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;

namespace ShelterCare.IntegrationTests.ShelterCareApi;
public class ShelterCareApiFactory : WebApplicationFactory<IApiMarker>,IAsyncLifetime
{
    private ITestOutputHelper _testOutputHelper;

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
           .WithDatabase("ShelterApiTestDb")
           .WithPassword("admin")
           .WithUsername("sa")
           .Build();
    
    public async Task InitializeAsync()
    {
        try
        {
            await _postgreSqlContainer.StartAsync();
            string uuidExtension = await File.ReadAllTextAsync("./Docker/enable_uuid_ossp.sql");
            string initScript = await File.ReadAllTextAsync("./Docker/tables.init.sql");
            await _postgreSqlContainer.ExecScriptAsync(uuidExtension);
            await _postgreSqlContainer.ExecScriptAsync(initScript);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public ShelterCareApiFactory SetOutPut(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        return this;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<ILoggerFactory>();
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ILoggerFactory>();

            // Clear default IDbConnection services
            services.RemoveAll(typeof(IDbConnection));

            services.AddSingleton<IDbConnection>(instance =>
            {
                return new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
            });
        });

        builder.ConfigureLogging(config =>
        {
            config.ClearProviders();

            config.SetMinimumLevel(LogLevel.Debug);

            config.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(_testOutputHelper));
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}
