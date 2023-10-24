using MediatR;
namespace ShelterCare.Application;
public class DeleteAnimalOwnerByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
}
