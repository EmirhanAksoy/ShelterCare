using MediatR;
namespace ShelterCare.Application;
public class DeleteAreaByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
}
