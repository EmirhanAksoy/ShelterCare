using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ShelterCare.IntegrationTests.FakeApis;

public class FakeNationalIdentityApi : IDisposable
{

    private WireMockServer _wireMockServer;

    public FakeNationalIdentityApi()
    {

        _wireMockServer = WireMockServer.Start();
    }

    public void SetupEndpoint(string nationalId)
    {
        _wireMockServer.Given(Request.Create()
            .WithPath($"/national-id/check/{nationalId}")
            .UsingGet())
            .RespondWith(Response.Create()
            .WithBody("{ confirmed : true }"));
    }

    public void Dispose()
    {
        _wireMockServer.Stop();
        _wireMockServer.Dispose();
    }
}
