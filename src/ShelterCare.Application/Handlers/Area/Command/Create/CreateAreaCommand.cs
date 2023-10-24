using MediatR;
namespace ShelterCare.Application;
public class CreateAreaCommand : IRequest<Response<Area>>
{
    public Guid ShelterId { get; set; }
    public string Name { get; set; }
    public Double AreaInSquareMeters { get; set; }

}
