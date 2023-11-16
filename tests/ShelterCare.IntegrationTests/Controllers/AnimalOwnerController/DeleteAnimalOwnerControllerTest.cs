namespace ShelterCare.IntegrationTests.Controllers.AnimalOwnerController;

public class DeleteAnimalOwnerControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<AnimalOwnerCreateRequest> _animalOwnerGenerator = new Faker<AnimalOwnerCreateRequest>()
      .RuleFor(x => x.Fullname, faker => ValidAnimalOwnerMapV1.FullName)
      .RuleFor(x => x.NationalId, faker => ValidAnimalOwnerMapV1.OwnerNationalId)
      .RuleFor(x => x.EmailAddress, faker => faker.Person.Email)
      .RuleFor(x => x.PhoneNumber, faker => faker.Person.Phone);
    public DeleteAnimalOwnerControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Delete Existing Animal Owner")]
    public async Task Delete_Existing_Animal_Owner()
    {
        // Arrange
        AnimalOwnerCreateRequest animalOwnerCreateRequest = _animalOwnerGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(AnimalOwnerRoutes.Delete.Replace("{id}", animalOwnerCreateResponse.Data.Id.ToString()));
        Response<bool> animalOwnerDeleteResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<bool>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalOwnerDeleteResponse.Errors.Count.Should().Be(0);
        animalOwnerDeleteResponse.Data.Should().Be(true);

    }

    [Fact(DisplayName = "Delete Animal Owner With Invalid Id")]
    public async Task Delete_Animal_Owner_With_Invalid_Id()
    {
        // Arrange
        const string invalidId = "XXXX";

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(AnimalOwnerRoutes.Delete.Replace("{id}", invalidId));
        Response<bool?> animalOwnerDeleteResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<bool?>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalOwnerDeleteResponse.ErrorCode.Should().Be(ValidationError.Code);
        animalOwnerDeleteResponse.Errors.Count.Should().Be(1);
        animalOwnerDeleteResponse.Errors.FirstOrDefault().Should().Be($"The value '{invalidId}' is not valid.");
    }

    [Fact(DisplayName = "Delete Not Existing Animal Owner")]
    public async Task Delete_Not_Exist_Animal_Owner()
    {
        // Arrange
        string notExistingId = Guid.NewGuid().ToString();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(AnimalOwnerRoutes.Delete.Replace("{id}", notExistingId));
        Response<bool?> animalOwnerDeleteResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<bool?>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        animalOwnerDeleteResponse.ErrorCode.Should().Be(AnimalOwnerNotFound.Code);
        animalOwnerDeleteResponse.Errors.Count.Should().Be(1);
        animalOwnerDeleteResponse.Errors.FirstOrDefault().Should().Be(AnimalOwnerNotFound.Message);
    }

}
