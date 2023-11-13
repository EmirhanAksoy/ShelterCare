namespace ShelterCare.Application;

public class GetAllSheltersQueryHandler : IRequestHandler<GetAllSheltersQuery, Response<List<Shelter>>>
{

    private readonly IShelterRepository _shelterRepository;
    private readonly ILogger<GetAllSheltersQueryHandler> _logger;

    public GetAllSheltersQueryHandler(IShelterRepository shelterRepository, ILogger<GetAllSheltersQueryHandler> logger)
    {
        _shelterRepository = shelterRepository;
        _logger = logger;
    }

    public async Task<Response<List<Shelter>>> Handle(GetAllSheltersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Response<List<Shelter>>.SuccessResult(await _shelterRepository.GetAll());
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAllSheltersQueryFailed.EventId, exception, "{Code} {Message}", GetAllSheltersQueryFailed.Code, GetAllSheltersQueryFailed.Message);
            return Response<List<Shelter>>.ErrorResult(GetAllSheltersQueryFailed.Code, GetAllSheltersQueryFailed.Message);
        }
    }
}
