namespace ShelterCare.Application;
public class GetAllAnimalQueryHandler : IRequestHandler<GetAllAnimalQuery,Response<List<Animal>>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly ILogger<GetAllAnimalQueryHandler> _logger;

    public GetAllAnimalQueryHandler(IAnimalRepository animalRepository, ILogger<GetAllAnimalQueryHandler> logger)
    {
        _animalRepository = animalRepository;
        _logger = logger;
    }

    public async Task<Response<List<Animal>>> Handle(GetAllAnimalQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Response<List<Animal>>.SuccessResult(await _animalRepository.GetAll());
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAllAnimalQueryFailed.EventId, exception, "{Code} {Message}", GetAllAnimalQueryFailed.Code, GetAllAnimalQueryFailed.Message);
            return Response<List<Animal>>.ErrorResult(GetAllAnimalQueryFailed.Code, GetAllAnimalQueryFailed.Message);
        }
    }
}
