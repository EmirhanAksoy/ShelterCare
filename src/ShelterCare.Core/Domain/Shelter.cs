using ShelterCare.Core.Domain.Base;

namespace ShelterCare.Core.Domain;

public class Shelter : Entity
{
    public string Name { get; set; }
    public string OwnerFullName { get; set; }
    public DateTime FoundationDate { get; set; }
    public double TotalAreaInSquareMeters { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
}

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
