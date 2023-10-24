using MediatR;
namespace ShelterCare.Application;
public class GetAllEmployeeQuery : IRequest<Response<List<Employee>>>
{
}
