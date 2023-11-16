using System.Text;
using System.Text.Json;

namespace ShelterCare.IntegrationTests.Controllers.AnimalOwnerController;

public class UpdateAnimalOwnerControllerTest : IClassFixture<ShelterCareApiFactory>
{
    private readonly HttpClient _httpClient;
    private readonly ShelterCareApiFactory _shelterCareApiFactory;
    private readonly Faker<AnimalOwnerUpdateRequest> _animaIOwnerGenerator = new Faker<AnimalOwnerUpdateRequest>()
        .RuleFor(x => x.Fullname, faker => ValidAnimalOwnerMapV1.FullName)
        .RuleFor(x => x.NationalId, faker => ValidAnimalOwnerMapV1.OwnerNationalId)
        .RuleFor(x => x.EmailAddress, faker => faker.Person.Email)
        .RuleFor(x => x.PhoneNumber, faker => faker.Person.Phone);
    public UpdateAnimalOwnerControllerTest(ITestOutputHelper testOutputHelper, ShelterCareApiFactory shelterCareApiFactory)
    {
        _shelterCareApiFactory = shelterCareApiFactory.SetOutPut(testOutputHelper);
        _httpClient = _shelterCareApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Update Animal Owner With Valid Input And Valid National Id")]
    public async Task Update_Animal_Owner_With_Valid_Input_And_Valid_NationalId()
    {
        // Arrange
        AnimalOwnerUpdateRequest animalOwnerCreateRequest = _animaIOwnerGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Update, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        animalOwnerCreateResponse.Data.Fullname = ValidAnimalOwnerMapV2.FullName;
        animalOwnerCreateResponse.Data.NationalId = ValidAnimalOwnerMapV2.OwnerNationalId;
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(AnimalOwnerRoutes.Update, new AnimalOwnerUpdateRequest()
        {
            EmailAddress = animalOwnerCreateResponse.Data.EmailAddress,
            Fullname = animalOwnerCreateResponse.Data.Fullname,
            Id = animalOwnerCreateResponse.Data.Id,
            NationalId = animalOwnerCreateResponse.Data.NationalId,
            PhoneNumber = animalOwnerCreateResponse.Data.PhoneNumber
        });
        Response<AnimalOwner> animalOwnerUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        animalOwnerUpdateResponse.Data.Should().NotBeNull();
        animalOwnerUpdateResponse.Data.Id.Should().NotBeEmpty();
        animalOwnerUpdateResponse.Data.NationalId.Should().NotBeEmpty();
        animalOwnerUpdateResponse.Data.NationalId.Should().Be(ValidAnimalOwnerMapV2.OwnerNationalId);
        animalOwnerUpdateResponse.Data.Fullname.Should().NotBeEmpty();
        animalOwnerUpdateResponse.Data.Fullname.Should().Be(ValidAnimalOwnerMapV2.FullName);
        animalOwnerUpdateResponse.Data.UpdateDate.Should().NotBe(DateTime.MinValue);
        animalOwnerUpdateResponse.Data.UpdateUserId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Update Animal Owner With Valid Input And Invalid National Id")]
    public async Task Update_Animal_Owner_With_Valid_Input_And_Invalid_NationalId()
    {
        // Arrange
        AnimalOwnerUpdateRequest animalOwnerCreateRequest = _animaIOwnerGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Update, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        animalOwnerCreateResponse.Data.NationalId = "XXXX";
        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(AnimalOwnerRoutes.Update, new AnimalOwnerUpdateRequest()
        {
            EmailAddress = animalOwnerCreateResponse.Data.EmailAddress,
            Fullname = animalOwnerCreateResponse.Data.Fullname,
            Id = animalOwnerCreateResponse.Data.Id,
            NationalId = animalOwnerCreateResponse.Data.NationalId,
            PhoneNumber = animalOwnerCreateResponse.Data.PhoneNumber
        });
        Response<AnimalOwner> animalOwnerUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalOwnerUpdateResponse.Data.Should().BeNull();
        animalOwnerUpdateResponse.Errors.Count.Should().BeGreaterThan(0);
        animalOwnerUpdateResponse.Errors.FirstOrDefault().Should().Be(AnimalOwnerConfirmationFailed.Message);
        animalOwnerUpdateResponse.ErrorCode.Should().Be(AnimalOwnerConfirmationFailed.Code);
    }

    [Fact(DisplayName = "Update Animal Owner With Invalid Input And Valid National Id")]
    public async Task Update_Animal_Owner_With_Invalid_Input_And_Valid_NationalId()
    {
        // Arrange
        AnimalOwnerUpdateRequest animalOwnerCreateRequest = _animaIOwnerGenerator.Generate();
        HttpResponseMessage createHttpResponseMessage = await _httpClient.PostAsJsonAsync(AnimalOwnerRoutes.Update, animalOwnerCreateRequest);
        Response<AnimalOwner> animalOwnerCreateResponse = await createHttpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        animalOwnerCreateResponse.Data.EmailAddress = string.Empty;
        // Act
        using StringContent jsonContent = new(
        JsonSerializer.Serialize(animalOwnerCreateResponse.Data),
        Encoding.UTF8,
        "application/json");
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(AnimalOwnerRoutes.Update, new AnimalOwnerUpdateRequest()
        {
            EmailAddress = animalOwnerCreateResponse.Data.EmailAddress,
            Fullname = animalOwnerCreateResponse.Data.Fullname,
            Id = animalOwnerCreateResponse.Data.Id,
            NationalId = animalOwnerCreateResponse.Data.NationalId,
            PhoneNumber = animalOwnerCreateResponse.Data.PhoneNumber
        });
        Response<AnimalOwner> animalOwnerUpdateResponse = await httpResponseMessage.Content.ReadFromJsonAsync<Response<AnimalOwner>>();
        //Assert
        httpResponseMessage.Should().NotBeNull();
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        animalOwnerUpdateResponse.Data.Should().BeNull();
        animalOwnerUpdateResponse.Errors.Count.Should().BeGreaterThan(0);
        animalOwnerUpdateResponse.ErrorCode.Should().Be(ValidationError.Code);
    }
}


