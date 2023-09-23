using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class CreateShelterCommandHandler : IRequestHandler<CreateShelterCommand, Response<Shelter>>
{

    private readonly IShelterRepository _shelterRepository;
    private readonly ILogger<CreateShelterCommandHandler> _logger;

    public CreateShelterCommandHandler(IShelterRepository shelterRepository, ILogger<CreateShelterCommandHandler> logger)
    {
        _shelterRepository = shelterRepository;
        _logger = logger;
    }


    public Task<Response<Shelter>> Handle(CreateShelterCommand request, CancellationToken cancellationToken)
    {
        // DO VALIDATION 

        // CREATE AND RETURN

        throw new NotImplementedException();
    }
}
