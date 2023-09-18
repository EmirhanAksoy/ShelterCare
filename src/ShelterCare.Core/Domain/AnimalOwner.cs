using ShelterCare.Core.Domain.Base;

namespace ShelterCare.Core.Domain;

public class AnimalOwner : Entity
{
    public string ShelterId { get; set; }
    public string Fullname { get; set; }
    public string NationalId { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
}
