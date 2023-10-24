using MediatR;
namespace ShelterCare.Application;
public class GetEmployeeByIdQuery : IRequest<Response<Employee>>
{
    public Guid Id { get; set; }
}
