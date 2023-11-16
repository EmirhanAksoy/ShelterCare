namespace ShelterCare.IntegrationTests.Controllers.AnimalController;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class GetAllAnimalsControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<AnimalCreateRequest> _animalGenerator = new Faker<AnimalCreateRequest>()
        .RuleFor(x => x.IsDisabled, faker => false)
        .RuleFor(x => x.IsNeutered, faker => true)
        .RuleFor(x => x.DateOfBirth, faker => DateTime.UtcNow.AddYears(-1))
        .RuleFor(x => x.JoiningDate, faker => DateTime.UtcNow)
        .RuleFor(x => x.Name, faker => ValidAnimalOwnerMapV1.LOKI)
        .RuleFor(x => x.UniqueIdentifier, faker => ValidAnimalOwnerMapV1.LOKI_IDENTIFIER);
    private readonly Faker<ShelterCreateRequest> _shelterGenerator = new Faker<ShelterCreateRequest>()
        .RuleFor(x => x.OwnerFullName, faker => faker.Person.FullName.SingleQuotes())
        .RuleFor(x => x.Website, faker => faker.Person.Email)
        .RuleFor(x => x.TotalAreaInSquareMeters, faker => 10000)
        .RuleFor(x => x.FoundationDate, faker => faker.Date.Recent())
        .RuleFor(x => x.Address, faker => faker.Address.FullAddress().SingleQuotes())
        .RuleFor(x => x.Name, faker => faker.Company.CompanyName().SingleQuotes());

    private readonly Faker<AnimalOwnerCreateRequest> _animalOwnerGenerator = new Faker<AnimalOwnerCreateRequest>()
        .RuleFor(x => x.Fullname, faker => ValidAnimalOwnerMapV1.FullName)
        .RuleFor(x => x.NationalId, faker => ValidAnimalOwnerMapV1.OwnerNationalId)
        .RuleFor(x => x.EmailAddress, faker => faker.Person.Email)
        .RuleFor(x => x.PhoneNumber, faker => faker.Person.Phone);

    private readonly Faker<AnimalSpecieCreateRequest> _animaSpecieGenerator = new Faker<AnimalSpecieCreateRequest>()
        .RuleFor(x => x.Name, faker => faker.Person.Avatar);
    public GetAllAnimalsControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Get All Animals With Existing Records"), Priority(2)]
    public async Task Get_All_Animals_With_Existing_Records()
    {
        // Arrange
        List<Response<Animal>> animals = new();
        for (int i = 0; i < 10; i++)
        {
            AnimalCreateRequest animalCreateRequest = _animalGenerator.Generate();

            ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
            HttpResponseMessage shelterHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
            Response<Shelter> shelterCreateResponse = await shelterHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

            AnimalOwnerCreateRequest animalOwnerCreateRequest = _animalOwnerGenerator.Generate();
            HttpResponseMessage animalOwnerHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
            Response<AnimalOwner> animalOwnerCreateResponse = await animalOwnerHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

            AnimalSpecieCreateRequest animalSpecieCreateRequest = _animaSpecieGenerator.Generate();
            HttpResponseMessage animalSpecieHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalSpecieRoutes.Create, animalSpecieCreateRequest);
            Response<AnimalSpecie> animalSpecieCreateResponse = await animalSpecieHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalSpecie>>();

            animalCreateRequest.ShelterId = shelterCreateResponse.Data.Id;
            animalCreateRequest.OwnerId = animalOwnerCreateResponse.Data.Id;
            animalCreateRequest.AnimalSpecieId = animalSpecieCreateResponse.Data.Id;


            var response = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
            var animalResponse = await response.Content.ReadFromJsonAsync<Response<Animal>>();
            animals.Add(animalResponse);
        }

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalRoutes.GetAll);
        Response<List<Animal>> animalResponses = await httpResponseMessage.Content.ReadFromJsonAsync<Response<List<Animal>>>();

        // Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalResponses.Data.Should().NotBeNull();
        // 1 record from seed data
        animalResponses.Data.Count.Should().Be(animals.Count + 1);
        animalResponses.Data.TrueForAll(x => animals.Any(z => z.Data.Id.Equals(x.Id)));
        animalResponses.Data.TrueForAll(x => animals.Any(z => z.Data.Name.Equals(x.Name)));
        animalResponses.Data.TrueForAll(x => animals.Any(z => z.Data.UniqueIdentifier.Equals(x.UniqueIdentifier)));
        animalResponses.Data.TrueForAll(x => animals.Any(z => z.Data.OwnerId.Equals(x.OwnerId)));
        animalResponses.Data.TrueForAll(x => animals.Any(z => z.Data.AnimalSpecieId.Equals(x.AnimalSpecieId)));
        animalResponses.Data.TrueForAll(x => animals.Any(z => z.Data.ShelterId.Equals(x.ShelterId)));
    }

    [Fact(DisplayName = "Get All Animals With No Records"), Priority(1)]
    public async Task Get_All_Animals_With_No_Records()
    {
        // Arrange

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalRoutes.GetAll);
        Response<List<Animal>> animalResponses = await httpResponseMessage.Content.ReadFromJsonAsync<Response<List<Animal>>>();
        // Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalResponses.Data.Should().NotBeNull();
        // 3 record from seed data
        animalResponses.Data.Count.Should().Be(1);
    }

    
}
