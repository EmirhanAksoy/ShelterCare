namespace ShelterCare.Application;

public class GetShelterByIdQueryHandler : IRequestHandler<GetShelterByIdQuery, Response<Shelter>>
{
    private readonly IShelterRepository _shelterRepository;
    private readonly ILogger<GetShelterByIdQueryHandler> _logger;

    public GetShelterByIdQueryHandler(IShelterRepository shelterRepository, ILogger<GetShelterByIdQueryHandler> logger)
    {
        _shelterRepository = shelterRepository;
        _logger = logger;
    }
    public async Task<Response<Shelter>> Handle(GetShelterByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            GetShelterByIdQueryValidator validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {shelterId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return Response<Shelter>.ErrorResult(ValidationError.Code, errorMessages);
            }
            Shelter shelter = await _shelterRepository.Get(request.Id);
            if (shelter is null)
            {
                _logger.LogError(ShelterNotFound.EventId, "{Code} {Message}  {shelterId}", ShelterNotFound.Code, ShelterNotFound.Message, request.Id.ToString());
                return Response<Shelter>.ErrorResult(ShelterNotFound.Code, ShelterNotFound.Message);
            }
            return Response<Shelter>.SuccessResult(shelter);
        }
        catch (Exception exception)
        {
            _logger.LogError(GetShelterByIdQueryFailed.EventId, exception, "{Code} {Message} {shelterId}", GetShelterByIdQueryFailed.Code, GetShelterByIdQueryFailed.Message, request.Id.ToString());
            return Response<Shelter>.ErrorResult(GetShelterByIdQueryFailed.Code, GetShelterByIdQueryFailed.Message);
        }
    }
}
