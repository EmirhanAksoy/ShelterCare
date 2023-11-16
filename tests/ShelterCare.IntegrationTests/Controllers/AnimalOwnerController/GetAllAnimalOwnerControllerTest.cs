namespace ShelterCare.IntegrationTests.Controllers.AnimalOwnerController;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class GetAllAnimalOwnerControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<AnimalOwnerCreateRequest> _animaIOwnerGenerator = new Faker<AnimalOwnerCreateRequest>()
       .RuleFor(x => x.Fullname, faker => ValidAnimalOwnerMapV1.FullName)
       .RuleFor(x => x.NationalId, faker => ValidAnimalOwnerMapV1.OwnerNationalId)
       .RuleFor(x => x.EmailAddress, faker => faker.Person.Email)
       .RuleFor(x => x.PhoneNumber, faker => faker.Person.Phone);
    public GetAllAnimalOwnerControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Get All Animal Owners With Existing Records"),Priority(2)]
    public async Task Get_All_Animal_Owners_With_Existing_Records()
    {
        // Arrange
        List<Response<AnimalOwner>> animalOwners = new();
        for (int i = 0; i < 10; i++)
        {
            AnimalOwnerCreateRequest animalOwnerCreateRequest = _animaIOwnerGenerator.Generate();
            var response = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Create, animalOwnerCreateRequest);
            var animalOwnerResponse = await response.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
            animalOwners.Add(animalOwnerResponse);
        }

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalOwnerRoutes.GetAll);
        Response<List<AnimalOwner>> animalOwnerResponses = await httpResponseMessage.Content.ReadFromJsonAsync<Response<List<AnimalOwner>>>();

        // Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalOwnerResponses.Data.Should().NotBeNull();
        // 1 record from seed data
        animalOwnerResponses.Data.Count.Should().Be(animalOwners.Count + 1);
        animalOwnerResponses.Data.TrueForAll(x => animalOwners.Any(z => z.Data.Id.Equals(x.Id)));
        animalOwnerResponses.Data.TrueForAll(x => animalOwners.Any(z => z.Data.Fullname.Equals(x.Fullname)));
        animalOwnerResponses.Data.TrueForAll(x => animalOwners.Any(z => z.Data.EmailAddress.Equals(x.EmailAddress)));
        animalOwnerResponses.Data.TrueForAll(x => animalOwners.Any(z => z.Data.NationalId.Equals(x.NationalId)));
    }

    [Fact(DisplayName = "Get All Animal Owners With No Records"),Priority(1)]
    public async Task Get_All_Animal_Owners_With_No_Records()
    {
        // Arrange

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(AnimalOwnerRoutes.GetAll);
        Response<List<AnimalOwner>> animalOwnerResponses = await httpResponseMessage.Content.ReadFromJsonAsync<Response<List<AnimalOwner>>>();
        // Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalOwnerResponses.Data.Should().NotBeNull();
        // 1 record from seed data
        animalOwnerResponses.Data.Count.Should().Be(1);
    }
}

