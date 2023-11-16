namespace ShelterCare.IntegrationTests.Controllers.AnimalController;
public class UpdateAnimalControllerTest : IClassFixture<ShelterCareApiFactory>
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

    public UpdateAnimalControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Update Animal With Valid Input,Valid Animal Identifier,Matching Owner")]
    public async Task Update_Animal_With_Valid_Input_Valid_Animal_Identifier_Matching_Owner()
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
        HttpResponseMessage animaCreateHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
        Response<Animal> animalCreateResponse = await animaCreateHttpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();

        animalCreateResponse.Data.UniqueIdentifier = ValidAnimalOwnerMapV1.LOKUM_IDENTIFIER;
        animalCreateResponse.Data.Name = ValidAnimalOwnerMapV1.LOKUM;

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(AnimalRoutes.Update, animalCreateResponse.Data);
        Response<Animal> animalUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalUpdateResponse.Data.Should().NotBeNull();
        animalUpdateResponse.Data.Id.Should().NotBeEmpty();
        animalUpdateResponse.Data.UniqueIdentifier.Should().NotBeEmpty();
        animalUpdateResponse.Data.UniqueIdentifier.Should().Be(ValidAnimalOwnerMapV1.LOKUM_IDENTIFIER);
        animalUpdateResponse.Data.Name.Should().NotBeEmpty();
        animalUpdateResponse.Data.Name.Should().Be(ValidAnimalOwnerMapV1.LOKUM);
        animalUpdateResponse.Data.CreateDate.Should().NotBe(DateTime.MinValue);
        animalUpdateResponse.Data.CreateUserId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Update Animal With Valid Input,Invalid Animal Identifier,Matching Owner")]
    public async Task Update_Animal_With_Valid_Input_Invalid_Animal_Identifier_Matching_Owner()
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
        HttpResponseMessage animaCreateHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
        Response<Animal> animalCreateResponse = await animaCreateHttpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();

        animalCreateResponse.Data.UniqueIdentifier = "XXXX";
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(AnimalRoutes.Update, animalCreateResponse.Data);
        Response<Animal> animalUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalUpdateResponse.Data.Should().BeNull();
        animalUpdateResponse.Errors.Count().Should().BeGreaterThan(0);
        animalUpdateResponse.Errors.FirstOrDefault().Should().Be(AnimalConfirmationFailed.Message);
        animalUpdateResponse.ErrorCode.Should().Be(AnimalConfirmationFailed.Code);
    }

    [Fact(DisplayName = "Update Animal With Valid Input,Invalid Animal Identifier,Not Matching Owner")]
    public async Task Update_Animal_With_Valid_Input_Valid_Animal_Identifier_Not_Matching_Owner()
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
        HttpResponseMessage animaCreateHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
        Response<Animal> animalCreateResponse = await animaCreateHttpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();

        AnimalOwnerCreateRequest animalNotMatchingOwnerCreateRequest = _animalOwnerGenerator
                .RuleFor(x => x.NationalId, faker => ValidAnimalOwnerMapV2.OwnerNationalId)
                .Generate();
        HttpResponseMessage animalNotMatchingOwnerHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalNotMatchingOwnerCreateRequest);
        Response<AnimalOwner> animalNotMatchingOwnerOwnerCreateResponse = await animalNotMatchingOwnerHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

        animalCreateResponse.Data.OwnerId = animalNotMatchingOwnerOwnerCreateResponse.Data.Id;
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(AnimalRoutes.Update, animalCreateResponse.Data);
        Response<Animal> animalUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalUpdateResponse.Data.Should().BeNull();
        animalUpdateResponse.Errors.Count().Should().BeGreaterThan(0);
        animalUpdateResponse.Errors.FirstOrDefault().Should().Be(OwnerOfAnimalConfirmationFailed.Message);
        animalUpdateResponse.ErrorCode.Should().Be(OwnerOfAnimalConfirmationFailed.Code);
    }

}

