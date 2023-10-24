using MediatR;
namespace ShelterCare.Application;
public class GetAreaByIdQuery : IRequest<Response<Area>>
{
    public Guid Id { get; set; }
}
