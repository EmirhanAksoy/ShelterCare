namespace ShelterCare.API.Contracts.Requests;

public class AnimalUpdateRequest
{
    public Guid Id { get; set; }
    public Guid ShelterId { get; set; }
    public Guid AnimalSpecieId { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UniqueIdentifier { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public bool IsNeutered { get; set; }
    public bool IsDisabled { get; set; }
}
