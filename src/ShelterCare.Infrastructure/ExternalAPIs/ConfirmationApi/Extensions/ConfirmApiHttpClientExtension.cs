using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ShelterCare.Infrastructure.ExternalAPIs;

public static class ConfirmApiHttpClientExtension
{
    public static void AddConfirmApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(ExternalApiKeys.ConfirmApi, (httpClient) =>
        {
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ConfirmationApi:BaseUrl") ?? "");
        });
    }
}
