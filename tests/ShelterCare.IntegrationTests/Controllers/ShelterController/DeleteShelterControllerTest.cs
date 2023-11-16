namespace ShelterCare.IntegrationTests.Controllers.ShelterController;

public class DeleteShelterControllerTest : IClassFixture<ShelterCareApiFactory>
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
    public DeleteShelterControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Delete Existing Shelter")]
    public async Task Delete_Existing_Shelter()
    {
        // Arrange
        ShelterCreateRequest shelterCreateRequest = _shelterGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(ShelterRoutes.Create, shelterCreateRequest);
        Response<Shelter> shelterCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<Shelter>>();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(ShelterRoutes.Delete.Replace("{id}", shelterCreateResponse.Data.Id.ToString()));
        Response<bool> shelterDeleteResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<bool>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        shelterDeleteResponse.Errors.Count.Should().Be(0);
        shelterDeleteResponse.Data.Should().Be(true);
        
    }

    [Fact(DisplayName = "Delete Sheter With Invalid Id")]
    public async Task Delete_Shelter_With_Invalid_Id()
    {
        // Arrange
        const string invalidId = "xxxx";

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(ShelterRoutes.Delete.Replace("{id}",invalidId));
        Response<bool?> shelterDeleteResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<bool?>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        shelterDeleteResponse.ErrorCode.Should().Be(ValidationError.Code);
        shelterDeleteResponse.Errors.Count.Should().Be(1);
        shelterDeleteResponse.Errors.FirstOrDefault().Should().Be($"The value '{invalidId}' is not valid.");
    }

    [Fact(DisplayName = "Delete Not Existing Shelter")]
    public async Task Delete_Not_Exist_Shelter()
    {
        // Arrange
        string notExistingId = Guid.NewGuid().ToString();

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync(ShelterRoutes.Delete.Replace("{id}", notExistingId));
        Response<bool?> shelterDeleteResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<bool?>>();

        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        shelterDeleteResponse.ErrorCode.Should().Be(ShelterNotFound.Code);
        shelterDeleteResponse.Errors.Count.Should().Be(1);
        shelterDeleteResponse.Errors.FirstOrDefault().Should().Be(ShelterNotFound.Message);
    }

}
