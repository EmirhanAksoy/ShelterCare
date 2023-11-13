namespace ShelterCare.Application;
public class GetAllAnimalOwnerQueryHandler : IRequestHandler<GetAllAnimalOwnerQuery, Response<List<AnimalOwner>>>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    private readonly ILogger<GetAllAnimalOwnerQueryHandler> _logger;

    public GetAllAnimalOwnerQueryHandler(IAnimalOwnerRepository animalOwnerRepository, ILogger<GetAllAnimalOwnerQueryHandler> logger)
    {
        _animalOwnerRepository = animalOwnerRepository;
        _logger = logger;
    }

    public async Task<Response<List<AnimalOwner>>> Handle(GetAllAnimalOwnerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Response<List<AnimalOwner>>.SuccessResult(await _animalOwnerRepository.GetAll());
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAllAnimalOwnersQueryFailed.EventId, exception, "{Code} {Message}", GetAllAnimalOwnersQueryFailed.Code, GetAllAnimalOwnersQueryFailed.Message);
            return Response<List<AnimalOwner>>.ErrorResult(GetAllAnimalOwnersQueryFailed.Code, GetAllAnimalOwnersQueryFailed.Message);
        }
    }
}
