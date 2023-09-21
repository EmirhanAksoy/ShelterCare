using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using ShelterCare.IntegrationTests.ShelterCareApi;
using Testcontainers.PostgreSql;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class CreateShelterControllerTest : IClassFixture<ShelterCareApiFactory>,IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
           .WithDatabase("ShelterApiTestDb")
           .WithPassword("admin")
           .WithUsername("sa")
           .Build();

    [Fact]
    public async Task Test()
    {

        await Task.Delay(300);
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}
