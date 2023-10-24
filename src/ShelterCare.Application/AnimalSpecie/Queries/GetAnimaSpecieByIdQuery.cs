using MediatR;

namespace ShelterCare.Application;

public class GetAnimaSpecieByIdQuery : IRequest<Response<AnimalSpecie>>
{
    public Guid Id { get; set; }
    public GetAnimaSpecieByIdQuery(Guid id)
    {
        Id = id;
    }
}
