namespace ShelterCare.IntegrationTests.Controllers.AnimalController;

public class GetByIdAnimaControllerTest : IClassFixture<ShelterCareApiFactory>
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
    public GetByIdAnimaControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Get Animal By Valid Id")]
    public async Task Get_Animal_By_Valid_Id()
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

        HttpResponseMessage animalCreateHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalRoutes.Create, animalCreateRequest);
        Response<Animal> animalCreateResponse = await animalCreateHttpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalRoutes.Get.Replace("{id}", animalCreateResponse.Data.Id.ToString()));
        Response<Animal> animaResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animaResponse.Data.Should().NotBeNull();
        animaResponse.Data.Id.Should().Be(animalCreateResponse.Data.Id);
        animaResponse.Data.UniqueIdentifier.Should().Be(animalCreateResponse.Data.UniqueIdentifier);
        animaResponse.Data.OwnerId.Should().Be(animalCreateResponse.Data.OwnerId);
        animaResponse.Data.AnimalSpecieId.Should().Be(animalCreateResponse.Data.AnimalSpecieId);
        animaResponse.Data.IsNeutered.Should().Be(animalCreateResponse.Data.IsNeutered);
    }

    [Fact(DisplayName = "Get Animal By Not Existing Id")]
    public async Task Get_Animal_By_Not_Existing_Id()
    {
        // Arrange
        string notExistingId = Guid.NewGuid().ToString();
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalRoutes.Get.Replace("{id}", notExistingId));
        Response<Animal> animalResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        animalResponse.ErrorCode.Should().Be(AnimalNotFound.Code);
        animalResponse.Errors.Count.Should().Be(1);
        animalResponse.Errors.FirstOrDefault().Should().Be(AnimalNotFound.Message);
    }

    [Fact(DisplayName = "Get Animal Owner By Invalid Id")]
    public async Task Get_Animal_Owner_By_Invalid_Id()
    {
        // Arrange
        string invalidId = "XXXXX";
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalRoutes.Get.Replace("{id}", invalidId));
        Response<Animal> animalResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<Animal>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalResponse.ErrorCode.Should().Be(ValidationError.Code);
        animalResponse.Errors.Count.Should().Be(1);
        animalResponse.Errors.FirstOrDefault().Should().Be($"The value '{invalidId}' is not valid.");
    }
}
