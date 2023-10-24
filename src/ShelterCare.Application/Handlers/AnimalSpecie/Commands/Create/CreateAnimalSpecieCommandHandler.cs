using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class CreateAnimalSpecieCommandHandler : IRequestHandler<CreateAnimalSpecieCommand, Response<AnimalSpecie>>
{
    private readonly IAnimalSpecieRepository _animalSpecieRepository;
    private readonly ILogger<CreateAnimalSpecieCommandHandler> _logger;

    public CreateAnimalSpecieCommandHandler(IAnimalSpecieRepository animalSpecieRepository, ILogger<CreateAnimalSpecieCommandHandler> logger)
    {
        _animalSpecieRepository = animalSpecieRepository;
        _logger = logger;
    }

    public async Task<Response<AnimalSpecie>> Handle(CreateAnimalSpecieCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CreateAnimalSpecieCommandValidation validationRules = new(_animalSpecieRepository);
            var validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages}", ValidationError.Code, ValidationError.Message, errorMessages);
                return Response<AnimalSpecie>.ErrorResult(ValidationError.Code, errorMessages);
            }
            AnimalSpecie animalSpecie = new()
            {
                Name = request.Name,
            };
            AnimalSpecie createdAnimalSpecie = await _animalSpecieRepository.Create(animalSpecie);
            _logger.LogInformation("Animal specie created successfully {@createdAnimalSpecie}", createdAnimalSpecie);
            return Response<AnimalSpecie>.SuccessResult(createdAnimalSpecie);
        }
        catch (Exception exception)
        {
            _logger.LogError(CreateShelterCommandFailed.EventId, exception, "{Code} {Message} {@animalSpecie}", CreateAnimalSpecieCommandFailed.Code, CreateAnimalSpecieCommandFailed.Message, request);
            return Response<AnimalSpecie>.ErrorResult(CreateAnimalSpecieCommandFailed.Code, CreateAnimalSpecieCommandFailed.Message);
        }
    }
}
