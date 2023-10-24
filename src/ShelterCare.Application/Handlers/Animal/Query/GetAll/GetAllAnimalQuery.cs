using MediatR;
namespace ShelterCare.Application;

public class GetAllAnimalQuery : IRequest<Response<List<Animal>>>
{
}
