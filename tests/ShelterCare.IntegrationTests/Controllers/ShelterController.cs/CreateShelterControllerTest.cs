using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using ShelterCare.IntegrationTests.ShelterCareApi;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class CreateShelterControllerTest : IClassFixture<ShelterCareApiFactory>
{

    private HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    public CreateShelterControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _testOutputHelper = testOutputHelper;
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Create Shelter Success")]
    public async Task Create_Success_Reponse()
    {
        // Arrange
        Faker<ShelterCreateRequest> _shelterGenerator = new Faker<ShelterCreateRequest>()
        .RuleFor(x => x.OwnerFullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Website, faker => faker.Person.Email)
        .RuleFor(x => x.TotalAreaInSquareMeters, faker => 10000)
        .RuleFor(x => x.FoundationDate, faker => faker.Date.Recent())
        .RuleFor(x => x.Address, faker => faker.Address.FullAddress())
        .RuleFor(x => x.Name, faker => faker.Company.CompanyName());
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        shelterCreateResponse.Data.Should().NotBeNull();
        shelterCreateResponse.Data.Id.Should().NotBeEmpty();

    }

    [Fact(DisplayName = "Create Shelter With Null Name")]
    public async Task Create_Shelter_Invalid_Null_Name()
    {
        // Arrange
        Faker<ShelterCreateRequest> _shelterGenerator = new Faker<ShelterCreateRequest>()
        .RuleFor(x => x.OwnerFullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Website, faker => faker.Person.Email)
        .RuleFor(x => x.TotalAreaInSquareMeters, faker => 10000)
        .RuleFor(x => x.FoundationDate, faker => faker.Date.Recent())
        .RuleFor(x => x.Address, faker => faker.Address.FullAddress())
        .RuleFor(x => x.Name, faker => null);
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        ValidationProblemDetails shelterCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        shelterCreateResponse.Status.Should().Be((int)StatusCodes.Status400BadRequest);

    }
    [Fact(DisplayName = "Create Shelter With Empty Name")]
    public async Task Create_Shelter_Invalid_Empty_Name()
    {
        // Arrange
        Faker<ShelterCreateRequest> _shelterGenerator = new Faker<ShelterCreateRequest>()
        .RuleFor(x => x.OwnerFullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Website, faker => faker.Person.Email)
        .RuleFor(x => x.TotalAreaInSquareMeters, faker => 10000)
        .RuleFor(x => x.FoundationDate, faker => faker.Date.Recent())
        .RuleFor(x => x.Address, faker => faker.Address.FullAddress())
        .RuleFor(x => x.Name, faker => string.Empty);
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        shelterCreateResponse.Errors.Should().NotBeNull();
        shelterCreateResponse.Errors.Count().Should().Be(1);
        shelterCreateResponse.Errors.FirstOrDefault().Should().Be("'Name' must not be empty.");
        shelterCreateResponse.ErrorCode.Should().Be(ValidationError.Code);
    }

}
