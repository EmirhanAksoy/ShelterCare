using MediatR;
namespace ShelterCare.Application;
public class DeleteEmployeeByIdCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
}
