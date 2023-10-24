using MediatR;

namespace ShelterCare.Application;
public class UpdateShelterCommand : IRequest<Response<Shelter>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string OwnerFullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; } = DateTime.MinValue;
    public double TotalAreaInSquareMeters { get; set; } = double.MinValue;
    public string Address { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
}
