namespace ShelterCare.API.Contracts.Requests;

public class AnimalOwnerUpdateRequest
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
