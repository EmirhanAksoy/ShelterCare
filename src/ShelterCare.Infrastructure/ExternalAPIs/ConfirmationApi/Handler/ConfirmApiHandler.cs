
using System.Net.Http.Json;
namespace ShelterCare.Infrastructure.ExternalAPIs;
public class ConfirmApiHandler
{
    
    private readonly IHttpClientFactory _httpClientFactory;
    public ConfirmApiHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> ConfirmOwner(string nationalId)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(ExternalApiKeys.ConfirmApi);
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"/national-id/confirm/{nationalId}");
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            NationalIdConfirmResponse response = await httpResponseMessage.Content.ReadFromJsonAsync<NationalIdConfirmResponse>();
            return response.Success;
        }
        return false;
    }

    public async Task<bool> ConfirmAnimal(string uniqueId)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(ExternalApiKeys.ConfirmApi);
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"/animal/id/confirm/{uniqueId}");
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            AnimalConfirmSuccessResponse response = await httpResponseMessage.Content.ReadFromJsonAsync<AnimalConfirmSuccessResponse>();
            return response.Success;
        }
        return false;
    }

    public async Task<bool> ConfirmAnimalOwner(string nationalId, string uniqueAnimalId)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(ExternalApiKeys.ConfirmApi);
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"/animal/owner/confirm/{nationalId}/{uniqueAnimalId}");
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            AnimalOwnerConfirmResponse response = await httpResponseMessage.Content.ReadFromJsonAsync<AnimalOwnerConfirmResponse>();
            return response.Success;
        }
        return false;
    }
}
