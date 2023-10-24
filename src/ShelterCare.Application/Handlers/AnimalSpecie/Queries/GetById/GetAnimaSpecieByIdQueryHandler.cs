using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class GetAnimaSpecieByIdQueryHandler : IRequestHandler<GetAnimaSpecieByIdQuery, Response<AnimalSpecie>>
{
    private readonly IAnimalSpecieRepository _animalSpecieRepository;
    private readonly ILogger<GetAnimaSpecieByIdQueryHandler> _logger;

    public GetAnimaSpecieByIdQueryHandler(IAnimalSpecieRepository animalSpecieRepository, ILogger<GetAnimaSpecieByIdQueryHandler> logger)
    {
        _animalSpecieRepository = animalSpecieRepository;
        _logger = logger;
    }
    public async Task<Response<AnimalSpecie>> Handle(GetAnimaSpecieByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            GetAnimaSpecieByIdQueryValidation validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {animalSpecieId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return Response<AnimalSpecie>.ErrorResult(ValidationError.Code, errorMessages);
            }
            AnimalSpecie animalSpecie = await _animalSpecieRepository.Get(request.Id);
            if (animalSpecie is null)
            {
                _logger.LogError(ShelterNotFound.EventId, "{Code} {Message} {animalSpecieId}", AnimalSpecieNotFound.Code, AnimalSpecieNotFound.Message, request.Id.ToString());
                return Response<AnimalSpecie>.ErrorResult(AnimalSpecieNotFound.Code, AnimalSpecieNotFound.Message);
            }
            return Response<AnimalSpecie>.SuccessResult(animalSpecie);
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAnimalSpecieByIdQueryFailed.EventId, exception, "{Code} {Message} {animalSpecieId}", GetAnimalSpecieByIdQueryFailed.Code, GetAnimalSpecieByIdQueryFailed.Message, request.Id.ToString());
            return Response<AnimalSpecie>.ErrorResult(GetAnimalSpecieByIdQueryFailed.Code, GetAnimalSpecieByIdQueryFailed.Message);
        }
    }
}
