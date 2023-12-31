﻿using Microsoft.AspNetCore.Http;

namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class CreateShelterControllerTest : IClassFixture<ShelterCareApiFactory>
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
    public CreateShelterControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Create Shelter With Valid Input")]
    public async Task Create_Shelter_With_Valid_Input()
    {
        // Arrange
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
    

    [Fact(DisplayName = "Create Shelter Invalid Name")]
    public async Task Create_Shelter_Invalid_Name()
    {
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Clone()
                .RuleFor(x => x.Name, faker => null)
                .Generate();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        shelterCreateResponse.Errors.Should().NotBeNull();
        shelterCreateResponse.Errors.Count.Should().Be(1);
        shelterCreateResponse.Errors.FirstOrDefault().Should().Be("The Name field is required.");
        shelterCreateResponse.ErrorCode.Should().Be(ValidationError.Code);
    }

    [Fact(DisplayName = "Create Shelter With Existing Name")]
    public async Task Create_Shelter_With_Existing_Name()
    {
        const string shelterName = "Happy Animals";
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Clone()
                .RuleFor(x => x.Name, faker => shelterName)
                .Generate();

        ShelterCreateRequest shelterCreateRequestWithSameName = _shelterGenerator.Clone()
               .RuleFor(x => x.Name, faker => shelterName)
               .Generate();

        await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequestWithSameName);
        Response<Shelter> shelterCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        shelterCreateResponse.Errors.Should().NotBeNull();
        shelterCreateResponse.Errors.Count.Should().Be(1);
        shelterCreateResponse.Errors.FirstOrDefault().Should().Be(ShelterNameAlreadyExists.Message);
        shelterCreateResponse.ErrorCode.Should().Be(ValidationError.Code);
    }

}
