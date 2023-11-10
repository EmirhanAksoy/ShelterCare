using MediatR;
namespace ShelterCare.Application;

public class GetAnimalByIdQuery : IRequest<Response<Animal>>
{
    public Guid Id { get; set; }

    public GetAnimalByIdQuery(Guid id)
    {
        Id = id;
    }
}
