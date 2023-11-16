namespace ShelterCare.IntegrationTests.Controllers.AnimalOwnerController;

public class GetByIdAnimaOwnerController : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<AnimalOwnerCreateRequest> _animaIOwnerGenerator = new Faker<AnimalOwnerCreateRequest>()
       .RuleFor(x => x.Fullname, faker => ValidAnimalOwnerMapV1.FullName)
       .RuleFor(x => x.NationalId, faker => ValidAnimalOwnerMapV1.OwnerNationalId)
       .RuleFor(x => x.EmailAddress, faker => faker.Person.Email)
       .RuleFor(x => x.PhoneNumber, faker => faker.Person.Phone);
    public GetByIdAnimaOwnerController(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Get Animal Owner By Valid Id")]
    public async Task Get_Animal_Owner_By_Valid_Id()
    {
        // Arrange
        AnimalOwnerCreateRequest AnimalOwnerCreateRequest = _animaIOwnerGenerator.Generate();
        HttpResponseMessage createAnimalOwnerHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, AnimalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await createAnimalOwnerHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalOwnerRoutes.Get.Replace("{id}", animalOwnerCreateResponse.Data.Id.ToString()));
        Response<AnimalOwner> animalOwnerResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalOwnerResponse.Data.Should().NotBeNull();
        animalOwnerResponse.Data.Id.Should().Be(animalOwnerCreateResponse.Data.Id);
        animalOwnerResponse.Data.Fullname.Should().Be(animalOwnerCreateResponse.Data.Fullname);
        animalOwnerResponse.Data.EmailAddress.Should().Be(animalOwnerCreateResponse.Data.EmailAddress);
        animalOwnerResponse.Data.NationalId.Should().Be(animalOwnerCreateResponse.Data.NationalId);
        animalOwnerResponse.Data.PhoneNumber.Should().Be(animalOwnerCreateResponse.Data.PhoneNumber);
    }

    [Fact(DisplayName = "Get Animal Owner By Not Existing Id")]
    public async Task Get_Animal_Owner_By_Not_Existing_Id()
    {
        // Arrange
        string notExistingId = Guid.NewGuid().ToString();
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalOwnerRoutes.Get.Replace("{id}", notExistingId));
        Response<AnimalOwner> animalOwnerResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        animalOwnerResponse.ErrorCode.Should().Be(AnimalOwnerNotFound.Code);
        animalOwnerResponse.Errors.Count.Should().Be(1);
        animalOwnerResponse.Errors.FirstOrDefault().Should().Be(AnimalOwnerNotFound.Message);
    }

    [Fact(DisplayName = "Get Animal Owner By Invalid Id")]
    public async Task Get_Animal_Owner_By_Invalid_Id()
    {
        // Arrange
        string invalidId = "XXXXX";
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalOwnerRoutes.Get.Replace("{id}", invalidId));
        Response<AnimalOwner> animalOwnerResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalOwnerResponse.ErrorCode.Should().Be(ValidationError.Code);
        animalOwnerResponse.Errors.Count.Should().Be(1);
        animalOwnerResponse.Errors.FirstOrDefault().Should().Be($"The value '{invalidId}' is not valid.");
    }
}

