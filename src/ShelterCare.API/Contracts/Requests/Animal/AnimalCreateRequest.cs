namespace ShelterCare.API.Contracts.Requests;

public class AnimalCreateRequest
{
    public Guid ShelterId { get; set; }
    public Guid? AreaId { get; set; }
    public Guid AnimalSpecieId { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string UniqueIdentifier { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public bool IsNeutered { get; set; }
    public bool IsDisabled { get; set; }
}
