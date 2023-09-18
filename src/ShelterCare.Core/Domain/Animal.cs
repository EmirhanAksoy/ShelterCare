using ShelterCare.Core.Domain.Base;

namespace ShelterCare.Core.Domain;

public class Animal : Entity
{
    public string AreaId { get; set; }
    public string AnimalSpecieId { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public bool IsNeutered { get; set; }
    public bool IsDisabled { get; set; }
}
