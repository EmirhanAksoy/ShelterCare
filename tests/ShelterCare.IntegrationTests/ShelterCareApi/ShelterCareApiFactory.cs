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
using ShelterCare.Infrastructure;
using ShelterCare.IntegrationTests.FakeApis;
using System.Data;
using Testcontainers.PostgreSql;

namespace ShelterCare.IntegrationTests.ShelterCareApi;
public class ShelterCareApiFactory : WebApplicationFactory<IApiMarker>,IAsyncLifetime
{
    private ITestOutputHelper _testOutputHelper;
    private readonly ConfirmationApiServer _confirmationApi = new();

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
           .WithDatabase("ShelterApiTestDb")
           .WithPassword("admin")
           .WithUsername("sa")
           .Build();
    
    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        string UUIDExtension = await File.ReadAllTextAsync("./Docker/enable_uuid_ossp.sql");
        string initScript = await File.ReadAllTextAsync("./Docker/tables.init.sql");
        await _postgreSqlContainer.ExecScriptAsync(UUIDExtension);
        await _postgreSqlContainer.ExecScriptAsync(initScript);
        _confirmationApi.Start();
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

            services.AddHttpClient(ExternalApiKeys.ConfirmApi, (httpClient) =>
            {
                httpClient.BaseAddress = _confirmationApi.Uri;
            });
        });

        builder.ConfigureLogging(config =>
        {
            config.ClearProviders();

            config.SetMinimumLevel(LogLevel.Debug);

            config.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(_testOutputHelper));
        });

        ;
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        _confirmationApi.Dispose();
        await _postgreSqlContainer.DisposeAsync();
    }
}
