using MediatR;

namespace ShelterCare.Application;

public class DeleteShelterByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
    public DeleteShelterByIdCommand(Guid id)
    {
        Id = id;
    }
}
