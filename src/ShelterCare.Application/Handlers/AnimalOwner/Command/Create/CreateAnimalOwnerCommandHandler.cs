using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class CreateAnimalOwnerCommandHandler : IRequestHandler<CreateAnimalOwnerCommand, Response<AnimalOwner>>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    private readonly ILogger<CreateAnimalOwnerCommandHandler> _logger;

    public CreateAnimalOwnerCommandHandler(ILogger<CreateAnimalOwnerCommandHandler> logger, IAnimalOwnerRepository animalOwnerRepository)
    {
        _logger = logger;
        _animalOwnerRepository = animalOwnerRepository;
    }

    public async Task<Response<AnimalOwner>> Handle(CreateAnimalOwnerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CreateAnimalOwnerCommandValidation validationRules = new(_animalOwnerRepository);
            var validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages}", ValidationError.Code, ValidationError.Message, errorMessages);
                return Response<AnimalOwner>.ErrorResult(ValidationError.Code, errorMessages);
            }
            AnimalOwner animalOwner = new()
            {
                Fullname = request.Fullname,
                EmailAddress = request.EmailAddress,
                NationalId = request.NationalId,
                PhoneNumber = request.PhoneNumber,
            };
            AnimalOwner createdAnimalOwner = await _animalOwnerRepository.Create(animalOwner);
            _logger.LogInformation("Animal owner created successfully {@createdAnimalOwner}", createdAnimalOwner);
            return Response<AnimalOwner>.SuccessResult(createdAnimalOwner);
        }
        catch (Exception exception)
        {
            _logger.LogError(CreateAnimalOwnerCommandFailed.EventId, exception, "{Code} {Message} {@animaOwner}", CreateAnimalOwnerCommandFailed.Code, CreateAnimalOwnerCommandFailed.Message, request);
            return Response<AnimalOwner>.ErrorResult(CreateAnimalOwnerCommandFailed.Code, CreateAnimalOwnerCommandFailed.Message);
        }
    }
}
