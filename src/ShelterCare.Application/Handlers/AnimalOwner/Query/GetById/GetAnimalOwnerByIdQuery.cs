using MediatR;
namespace ShelterCare.Application;
public class GetAnimalOwnerByIdQuery : IRequest<Response<AnimalOwner>>
{
    public Guid Id { get; set; }
}
