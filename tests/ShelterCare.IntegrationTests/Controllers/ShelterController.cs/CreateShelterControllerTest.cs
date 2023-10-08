using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using ShelterCare.IntegrationTests.ShelterCareApi;
using System.Collections;
using System.Net;
using System.Net.Http.Json;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class CreateShelterControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly Faker<ShelterCreateRequest> _shelterGenerator = new Faker<ShelterCreateRequest>()
        .RuleFor(x => x.OwnerFullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Website, faker => faker.Person.Email)
        .RuleFor(x => x.TotalAreaInSquareMeters, faker => 10000)
        .RuleFor(x => x.FoundationDate, faker => faker.Date.Recent())
        .RuleFor(x => x.Address, faker => faker.Address.FullAddress())
        .RuleFor(x => x.Name, faker => faker.Company.CompanyName());


    private HttpClient _httpClient;
    private ShelterCareApiFactory _ShelterCareApiFactory;
    public CreateShelterControllerTest(ShelterCareApiFactory shelterCareApiFactory)
    {
        _ShelterCareApiFactory = shelterCareApiFactory;
        _httpClient = shelterCareApiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_Success_Reponse()
    {
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        shelterCreateResponse.Data.Should().BeEquivalentTo(shelterCreateRequest);
        shelterCreateResponse.Data.Id.Should().NotBeEmpty();

    }
}
