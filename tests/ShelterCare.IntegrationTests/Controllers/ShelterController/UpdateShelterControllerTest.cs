using Bogus;
using FluentAssertions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using ShelterCare.IntegrationTests.ShelterCareApi;
using System.Net.Http.Json;
using System.Net;
using Xunit.Abstractions;
using System.Text.Json;
using System.Text;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class UpdateShelterControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<ShelterCreateRequest> _shelterGenerator = new Faker<ShelterCreateRequest>()
        .RuleFor(x => x.OwnerFullName, faker => faker.Person.FullName.SingleQuotes())
        .RuleFor(x => x.Website, faker => faker.Person.Email)
        .RuleFor(x => x.TotalAreaInSquareMeters, faker => 10000)
        .RuleFor(x => x.FoundationDate, faker => faker.Date.Recent())
        .RuleFor(x => x.Address, faker => faker.Address.FullAddress().SingleQuotes())
        .RuleFor(x => x.Name, faker => faker.Company.CompanyName().SingleQuotes());
    public UpdateShelterControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }


    [Fact(DisplayName = "Update Shelter Existing Shelter")]
    public async Task Update_Existing_Shelter()
    {
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();
        ShelterUpdateRequest shelterUpdateRequest = new()
        {
            Name = "Happy Animals",
            Address = shelterCreateResponse.Data.Address,
            FoundationDate = shelterCreateResponse.Data.FoundationDate,
            Id = shelterCreateResponse.Data.Id,
            OwnerFullName = shelterCreateResponse.Data.OwnerFullName,
            TotalAreaInSquareMeters = 2000,
            Website = "www.happy-animals.com"
        };
        // Act
        using StringContent jsonContent = new(
        JsonSerializer.Serialize(shelterUpdateRequest),
        Encoding.UTF8,
        "application/json");

        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsync(ShelterRoutes.Update, jsonContent);
        Response<Shelter> shelterUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        shelterUpdateResponse.Errors.Count.Should().Be(0);
        shelterUpdateResponse.Data.Name.Should().Be(shelterUpdateRequest.Name);
        shelterUpdateResponse.Data.TotalAreaInSquareMeters.Should().Be(shelterUpdateRequest.TotalAreaInSquareMeters);
        shelterUpdateResponse.Data.Website.Should().Be(shelterUpdateRequest.Website);
    }

    [Fact(DisplayName = "Update Shelter Not Existing Shelter")]
    public async Task Update_Not_Existing_Shelter()
    {
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();
        ShelterUpdateRequest shelterUpdateRequest = new()
        {
            Name = "Happy Animals",
            Address = shelterCreateResponse.Data.Address,
            FoundationDate = shelterCreateResponse.Data.FoundationDate,
            Id = Guid.NewGuid(),
            OwnerFullName = shelterCreateResponse.Data.OwnerFullName,
            TotalAreaInSquareMeters = 2000,
            Website = "www.happy-animals.com"
        };
        // Act
        using StringContent jsonContent = new(
        JsonSerializer.Serialize(shelterUpdateRequest),
        Encoding.UTF8,
        "application/json");

        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsync(ShelterRoutes.Update, jsonContent);
        Response<Shelter> shelterUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        shelterUpdateResponse.ErrorCode.Should().Be(ShelterNotFound.Code);
        shelterUpdateResponse.Errors.Count.Should().Be(1);
        shelterUpdateResponse.Errors.FirstOrDefault().Should().Be(ShelterNotFound.Message);
    }

    [Fact(DisplayName = "Update Shelter With Invalid Shelter Id")]
    public async Task Update_Not_Invalid_Shelter_Id()
    {
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();
        ShelterUpdateRequest shelterUpdateRequest = new()
        {
            Name = "Happy Animals",
            Address = shelterCreateResponse.Data.Address,
            FoundationDate = shelterCreateResponse.Data.FoundationDate,
            Id = Guid.Empty,
            OwnerFullName = shelterCreateResponse.Data.OwnerFullName,
            TotalAreaInSquareMeters = 2000,
            Website = "www.happy-animals.com"
        };
        // Act
        using StringContent jsonContent = new(
        JsonSerializer.Serialize(shelterUpdateRequest),
        Encoding.UTF8,
        "application/json");

        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsync(ShelterRoutes.Update, jsonContent);
        Response<Shelter> shelterUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        shelterUpdateResponse.Errors.Should().NotBeNull();
        shelterUpdateResponse.Errors.Count.Should().Be(1);
        shelterUpdateResponse.ErrorCode.Should().Be(ValidationError.Code);
        shelterUpdateResponse.Errors.FirstOrDefault().Should().Be("'Id' must not be empty.");
    }

}
