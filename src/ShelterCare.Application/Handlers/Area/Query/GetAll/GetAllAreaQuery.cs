using MediatR;

namespace ShelterCare.Application;

public class GetAllAreaQuery : IRequest<Response<List<Area>>>
{
}
