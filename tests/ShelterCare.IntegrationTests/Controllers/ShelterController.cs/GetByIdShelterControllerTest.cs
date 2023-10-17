using Bogus;
using FluentAssertions;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using ShelterCare.IntegrationTests.ShelterCareApi;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class GetByIdShelterControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<ShelterCreateRequest> _shelterGenerator = new Faker<ShelterCreateRequest>()
        .RuleFor(x => x.OwnerFullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Website, faker => faker.Person.Email)
        .RuleFor(x => x.TotalAreaInSquareMeters, faker => 10000)
        .RuleFor(x => x.FoundationDate, faker => faker.Date.Recent())
        .RuleFor(x => x.Address, faker => faker.Address.FullAddress())
        .RuleFor(x => x.Name, faker => faker.Company.CompanyName());
    public GetByIdShelterControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Get Shelter By Valid Id")]
    public async Task Get_Shelter_By_Valid_Id()
    {
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
        HttpResponseMessage createShelterHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await createShelterHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(ShelterRoutes.Get.Replace("{id}",shelterCreateResponse.Data.Id.ToString()));
        Response<Shelter> shelterResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        shelterResponse.Data.Should().NotBeNull();
        shelterResponse.Data.Id.Should().Be(shelterCreateResponse.Data.Id);
        shelterResponse.Data.Name.Should().Be(shelterCreateResponse.Data.Name);
        shelterResponse.Data.OwnerFullName.Should().Be(shelterCreateResponse.Data.OwnerFullName);
        shelterResponse.Data.TotalAreaInSquareMeters.Should().Be(shelterCreateResponse.Data.TotalAreaInSquareMeters);
        shelterResponse.Data.Website.Should().Be(shelterCreateResponse.Data.Website);
    }

    [Fact(DisplayName = "Get Shelter By Not Existing Id")]
    public async Task Get_Shelter_By_Not_Existing_Id()
    {
        // Arrange
        string notExistingId = Guid.NewGuid().ToString();
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(ShelterRoutes.Get.Replace("{id}", notExistingId));
        Response<Shelter> shelterResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        shelterResponse.ErrorCode.Should().Be(ShelterNotFound.Code);
        shelterResponse.Errors.Count.Should().Be(1);
        shelterResponse.Errors.FirstOrDefault().Should().Be("Shelter is not found");
    }

    [Fact(DisplayName = "Get Shelter By Invalid Id")]
    public async Task Get_Shelter_By_Invalid_Id()
    {
        // Arrange
        string invalidId = "xxxxx";
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(ShelterRoutes.Get.Replace("{id}", invalidId));
        Response<Shelter> shelterResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        shelterResponse.ErrorCode.Should().Be(ValidationError.Code);
        shelterResponse.Errors.Count.Should().Be(1);
        shelterResponse.Errors.FirstOrDefault().Should().Be("The value 'xxxxx' is not valid.");
    }
}
