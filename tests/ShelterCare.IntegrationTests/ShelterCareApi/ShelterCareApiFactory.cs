

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
using System;
using System.Data;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ShelterCare.IntegrationTests.ShelterCareApi;
public class ShelterCareApiFactory : IAsyncLifetime
{

    private readonly ITestOutputHelper _testOutputHelper;

    public ShelterCareApiFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
           .WithDatabase("ShelterApiTestDb")
           .WithPassword("admin")
           .WithUsername("sa")
           .WithPortBinding(5432, 5432)
           .Build();

    public WebApplicationFactory<IApiMarker> WebApplicationFactory { get; private set; }
    public async Task InitializeAsync()
    {
        try
        {
            await _postgreSqlContainer.StartAsync();
            await _postgreSqlContainer.ExecScriptAsync("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
            await _postgreSqlContainer.ExecScriptAsync("""

                CREATE TABLE IF NOT EXISTS Shelters (
                Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
                Name VARCHAR(255) NOT NULL,
                OwnerFullName VARCHAR(255),
                FoundationDate TIMESTAMP,
                TotalAreaInSquareMeters DOUBLE PRECISION,
                Address TEXT,
                Website VARCHAR(255),
                IsActive BOOLEAN NOT NULL,
                CreateDate TIMESTAMP NOT NULL,
                CreateUserId UUID NOT NULL,
                UpdateDate TIMESTAMP,
                UpdateUserId UUID
            );
            """);

            WebApplicationFactory = new WebApplicationFactory<IApiMarker>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureLogging(config =>
                {
                    config.ClearProviders();

                    config.SetMinimumLevel(LogLevel.Debug);

                    config.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(_testOutputHelper));
                });
                builder.ConfigureTestServices(services =>
                {
                    // Clear default IDbConnection services
                    services.RemoveAll(typeof(IDbConnection));

                    services.AddSingleton<IDbConnection>(instance =>
                    {
                        return new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
                    });
                });
            });
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}
