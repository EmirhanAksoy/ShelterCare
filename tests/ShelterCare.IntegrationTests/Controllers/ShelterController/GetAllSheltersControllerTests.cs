namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class GetAllSheltersControllerTests : IClassFixture<ShelterCareApiFactory>
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
    public GetAllSheltersControllerTests(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Get All Shelters With Existing Records")]
    public async Task Get_All_Shelters_With_Existing_Records()
    {
        // Arrange
        List<Response<Shelter>> shelters = new();
        for (int i = 0; i < 10; i++)
        {
            ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
            var response = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
            var shelterReponse = await response.Content.ReadFromJsonAsync<Response<Shelter>>();
            shelters.Add(shelterReponse);
        }

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(ShelterRoutes.GetAll);
        Response<List<Shelter>> shelterResponses = await httpResponseMessage.Content.ReadFromJsonAsync<Response<List<Shelter>>>();

        // Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        shelterResponses.Data.Should().NotBeNull();
        shelterResponses.Data.Count.Should().Be(shelters.Count);
        shelterResponses.Data.TrueForAll(x => shelters.Any(z => z.Data.Id.Equals(x.Id)));
        shelterResponses.Data.TrueForAll(x => shelters.Any(z => z.Data.Name.Equals(x.Name)));
        shelterResponses.Data.TrueForAll(x => shelters.Any(z => z.Data.Website.Equals(x.Website)));
        shelterResponses.Data.TrueForAll(x => shelters.Any(z => z.Data.OwnerFullName.Equals(x.OwnerFullName)));
    }

    [Fact(DisplayName = "Get All Shelters With No Records")]
    public async Task Get_All_Shelters_With_No_Records()
    {
        // Arrange

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(ShelterRoutes.GetAll);
        Response<List<Shelter>> shelterResponses = await httpResponseMessage.Content.ReadFromJsonAsync<Response<List<Shelter>>>();

        // Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        shelterResponses.Data.Should().NotBeNull();
        shelterResponses.Data.Count.Should().Be(0);
    }
}
