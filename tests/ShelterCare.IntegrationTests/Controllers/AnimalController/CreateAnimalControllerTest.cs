namespace ShelterCare.IntegrationTests.Controllers.AnimalController;

public class CreateAnimalControllerTest : IClassFixture<ShelterCareApiFactory>
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
        .RuleFor(x => x.Name, faker => "Lion");
        
    public CreateAnimalControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Create Animal With Valid Input,Valid Animal Identifier,Matching Owner")]
    public async Task Create_Animal_With_Valid_Input_Valid_Animal_Identifier_Matching_Owner()
    {
        // Arrange
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

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
        Response<Animal> animalCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalCreateResponse.Data.Should().NotBeNull();
        animalCreateResponse.Data.Id.Should().NotBeEmpty();
        animalCreateResponse.Data.UniqueIdentifier.Should().NotBeEmpty();
        animalCreateResponse.Data.UniqueIdentifier.Should().Be(animalCreateRequest.UniqueIdentifier);
        animalCreateResponse.Data.Name.Should().NotBeEmpty();
        animalCreateResponse.Data.Name.Should().Be(animalCreateRequest.Name);
        animalCreateResponse.Data.CreateDate.Should().NotBe(DateTime.MinValue);
        animalCreateResponse.Data.CreateUserId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Create Animal With Valid Input,Invalid Animal Identifier,Matching Owner")]
    public async Task Create_Animal_With_Valid_Input_Invalid_Animal_Identifier_Matching_Owner()
    {
        // Arrange
        AnimalCreateRequest animalCreateRequest = _animalGenerator.Clone()
                .RuleFor(x => x.UniqueIdentifier, faker => "XXXX")
                .Generate();

        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
        HttpResponseMessage shelterHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await shelterHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        AnimalOwnerCreateRequest animalOwnerCreateRequest = _animalOwnerGenerator.Generate();
        HttpResponseMessage animalOwnerHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await animalOwnerHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

        AnimalSpecieCreateRequest animalSpecieCreateRequest = _animaSpecieGenerator.Clone()
                .RuleFor(x => x.Name, faker => "Dragon")
                .Generate(); ;
        HttpResponseMessage animalSpecieHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalSpecieRoutes.Create, animalSpecieCreateRequest);
        Response<AnimalSpecie> animalSpecieCreateResponse = await animalSpecieHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalSpecie>>();

        animalCreateRequest.ShelterId = shelterCreateResponse.Data.Id;
        animalCreateRequest.OwnerId = animalOwnerCreateResponse.Data.Id;
        animalCreateRequest.AnimalSpecieId = animalSpecieCreateResponse.Data.Id;

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
        Response<Animal> animalCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalCreateResponse.Data.Should().BeNull();
        animalCreateResponse.Errors.Count().Should().BeGreaterThan(0);
        animalCreateResponse.Errors.FirstOrDefault().Should().Be(AnimalConfirmationFailed.Message);
        animalCreateResponse.ErrorCode.Should().Be(AnimalConfirmationFailed.Code);
    }

    [Fact(DisplayName = "Create Animal With Valid Input,Invalid Animal Identifier,Not Matching Owner")]
    public async Task Create_Animal_With_Valid_Input_Valid_Animal_Identifier_Not_Matching_Owner()
    {
        // Arrange
        AnimalCreateRequest animalCreateRequest = _animalGenerator.Generate();

        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
        HttpResponseMessage shelterHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await shelterHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        AnimalOwnerCreateRequest animalOwnerCreateRequest = _animalOwnerGenerator.Clone()
                .RuleFor(x => x.NationalId, faker =>ValidAnimalOwnerMapV2.OwnerNationalId)
                .Generate(); ;
        HttpResponseMessage animalOwnerHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await animalOwnerHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

        AnimalSpecieCreateRequest animalSpecieCreateRequest = _animaSpecieGenerator.Clone()
                .RuleFor(x => x.Name, faker => "Phoenix")
                .Generate(); ;
        HttpResponseMessage animalSpecieHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalSpecieRoutes.Create, animalSpecieCreateRequest);
        Response<AnimalSpecie> animalSpecieCreateResponse = await animalSpecieHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalSpecie>>();

        animalCreateRequest.ShelterId = shelterCreateResponse.Data.Id;
        animalCreateRequest.OwnerId = animalOwnerCreateResponse.Data.Id;
        animalCreateRequest.AnimalSpecieId = animalSpecieCreateResponse.Data.Id;

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
        Response<Animal> animalCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalCreateResponse.Data.Should().BeNull();
        animalCreateResponse.Errors.Count().Should().BeGreaterThan(0);
        animalCreateResponse.Errors.FirstOrDefault().Should().Be(OwnerOfAnimalConfirmationFailed.Message);
        animalCreateResponse.ErrorCode.Should().Be(OwnerOfAnimalConfirmationFailed.Code);
    }

}

