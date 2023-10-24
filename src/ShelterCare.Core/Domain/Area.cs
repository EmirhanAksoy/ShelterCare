using ShelterCare.Core.Domain.Base;

namespace ShelterCare.Core.Domain;

public class Area : Entity
{
    public Guid ShelterId { get; set; }
    public string Name { get; set; }
    public double AreaInSquareMeters { get; set; }
}
