using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using ShelterCare.IntegrationTests.ShelterCareApi;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class CreateShelterControllerTest : IClassFixture<ShelterCareApiFactory>,IAsyncLifetime
{
    private readonly IContainer container = new ContainerBuilder()
            .WithImage("postgres:latest")
            .WithEnvironment("POSTGRES_USER", "sa")
            .WithEnvironment("POSTGRES_PASSWORD", "admin")
            .WithEnvironment("POSTGRES_DB", "ShelterApiTestDb")
            .WithPortBinding(5555, 5432)
            .WithWaitStrategy(Wait.ForUnixContainer()
            .UntilPortIsAvailable(5432))
            .Build();


    [Fact]
    public async Task Test()
    {
        await Task.Delay(300);
    }

    public async Task InitializeAsync()
    {
        await container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await container.DisposeAsync();
    }
}
