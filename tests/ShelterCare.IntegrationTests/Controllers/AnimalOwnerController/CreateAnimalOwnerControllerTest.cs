

namespace ShelterCare.IntegrationTests.Controllers.AnimalOwnerController;

public class CreateAnimalOwnerControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<AnimalOwnerCreateRequest> _animaIOwnerGenerator = new Faker<AnimalOwnerCreateRequest>()
        .RuleFor(x => x.Fullname, faker => ValidAnimalOwnerMapV1.FullName)
        .RuleFor(x => x.NationalId, faker => ValidAnimalOwnerMapV1.OwnerNationalId)
        .RuleFor(x => x.EmailAddress, faker => faker.Person.Email)
        .RuleFor(x => x.PhoneNumber, faker => faker.Person.Phone);
    public CreateAnimalOwnerControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Create Animal Owner With Valid Input And Valid National Id")]
    public async Task Create_Animal_Owner_With_Valid_Input_And_Valid_NationalId()
    {
        // Arrange
        AnimalOwnerCreateRequest animalOwnerCreateRequest = _animaIOwnerGenerator.Generate();
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalOwnerCreateResponse.Data.Should().NotBeNull();
        animalOwnerCreateResponse.Data.Id.Should().NotBeEmpty();
        animalOwnerCreateResponse.Data.NationalId.Should().NotBeEmpty();
        animalOwnerCreateResponse.Data.NationalId.Should().Be(animalOwnerCreateRequest.NationalId);
        animalOwnerCreateResponse.Data.Fullname.Should().NotBeEmpty();
        animalOwnerCreateResponse.Data.Fullname.Should().Be(animalOwnerCreateRequest.Fullname);
        animalOwnerCreateResponse.Data.CreateDate.Should().NotBe(DateTime.MinValue);
        animalOwnerCreateResponse.Data.CreateUserId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Create Animal Owner With Valid Input And Invalid National Id")]
    public async Task Create_Animal_Owner_With_Valid_Input_And_Invalid_NationalId()
    {
        // Arrange
        AnimalOwnerCreateRequest animalOwnerCreateRequest = _animaIOwnerGenerator.Clone()
                .RuleFor(x => x.NationalId, faker => "XXXX")
                .Generate();
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalOwnerCreateResponse.Data.Should().BeNull();
        animalOwnerCreateResponse.Errors.Count.Should().BeGreaterThan(0);
        animalOwnerCreateResponse.Errors.FirstOrDefault().Should().Be(AnimalOwnerConfirmationFailed.Message);
        animalOwnerCreateResponse.ErrorCode.Should().Be(AnimalOwnerConfirmationFailed.Code);
    }

    [Fact(DisplayName = "Create Animal Owner With Invalid Input And Valid National Id")]
    public async Task Create_Animal_Owner_With_Invalid_Input_And_Valid_NationalId()
    {
        // Arrange
        AnimalOwnerCreateRequest animalOwnerCreateRequest = _animaIOwnerGenerator.Clone()
                .RuleFor(x => x.NationalId, faker =>ValidAnimalOwnerMapV1.OwnerNationalId)
                .RuleFor(x => x.EmailAddress, faker => string.Empty)
                .Generate();
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalOwnerCreateResponse.Data.Should().BeNull();
        animalOwnerCreateResponse.Errors.Count.Should().BeGreaterThan(0);
        animalOwnerCreateResponse.ErrorCode.Should().Be(ValidationError.Code);
    }
}

