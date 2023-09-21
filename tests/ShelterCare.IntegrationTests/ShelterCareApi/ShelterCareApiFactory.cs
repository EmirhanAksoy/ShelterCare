

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using ShelterCare.API.Marker;

namespace ShelterCare.IntegrationTests.ShelterCareApi;

public class ShelterCareApiFactory :  WebApplicationFactory<IApiMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(config =>
        {
            config.ClearProviders();
        });
    }
}
