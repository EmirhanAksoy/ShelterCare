using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using ShelterCare.IntegrationTests.FakeApis;
using ShelterCare.IntegrationTests.ShelterCareApi;
using Testcontainers.PostgreSql;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class CreateShelterControllerTest : IClassFixture<ShelterCareApiFactory>,IAsyncLifetime
{
    private readonly FakeNationalIdentityApi _fakeNationalIdentityApi = new();
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
        _fakeNationalIdentityApi.SetupEndpoint("1234567890");

    }

    public async Task DisposeAsync()
    {
        _fakeNationalIdentityApi.Dispose();
        await _postgreSqlContainer.DisposeAsync();
    }
}
