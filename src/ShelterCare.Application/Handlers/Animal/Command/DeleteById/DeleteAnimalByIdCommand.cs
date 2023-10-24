using MediatR;
namespace ShelterCare.Application;

public class DeleteAnimalByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
}
