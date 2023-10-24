using ShelterCare.Core.Domain.Base;

namespace ShelterCare.Core.Domain;

public class Employee : Entity
{
    public Guid ShelterId { get; set; }
    public string Fullname { get; set; }
    public string NationalId { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsOwner { get; set; }
    public DateTime DateOfStart { get; set; }
    public DateTime? DismissalDate { get; set; }
}
