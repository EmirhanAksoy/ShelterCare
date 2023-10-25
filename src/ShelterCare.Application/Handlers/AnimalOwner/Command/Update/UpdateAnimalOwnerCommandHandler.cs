using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class UpdateAnimalOwnerCommandHandler : IRequestHandler<UpdateAnimalOwnerCommand, Response<AnimalOwner>>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    private readonly ILogger<UpdateAnimalOwnerCommandHandler> _logger;

    public UpdateAnimalOwnerCommandHandler(ILogger<UpdateAnimalOwnerCommandHandler> logger, IAnimalOwnerRepository animalOwnerRepository)
    {
        _logger = logger;
        _animalOwnerRepository = animalOwnerRepository;
    }
    public async Task<Response<AnimalOwner>> Handle(UpdateAnimalOwnerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            UpdateAnimalOwnerCommandValidation validationRules = new();
            var validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages}", ValidationError.Code, ValidationError.Message, errorMessages);
                return Response<AnimalOwner>.ErrorResult(ValidationError.Code, errorMessages);
            }

            AnimalOwner existingAnimalOwner = await _animalOwnerRepository.Get(request.Id);
            if (existingAnimalOwner is null)
            {
                _logger.LogError(AnimalOwnerNotFound.EventId, "{Code} {Message}  {animaOwnerId}", AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message, request.Id.ToString());
                return Response<AnimalOwner>.ErrorResult(AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message);
            }

            if(!existingAnimalOwner.NationalId.Equals(request.NationalId))
            {
                bool isExists = await _animalOwnerRepository.CheckIfAnimalOwnerExists(request.NationalId);
                if(isExists)
                {
                    _logger.LogError(AnimalOwnerNationalIdAlreadyExists.EventId, "{Code} {Message}  {nationalId}", AnimalOwnerNationalIdAlreadyExists.Code, AnimalOwnerNationalIdAlreadyExists.Message, request.NationalId);
                    return Response<AnimalOwner>.ErrorResult(AnimalOwnerNationalIdAlreadyExists.Code, AnimalOwnerNationalIdAlreadyExists.Message);
                }
            }

            AnimalOwner animalOwner = new()
            {
                Id = request.Id,
                PhoneNumber = request.PhoneNumber,
                NationalId = request.NationalId,
                EmailAddress = request.EmailAddress,
                Fullname = request.Fullname,
            };
            AnimalOwner updatedAnimalOwner = await _animalOwnerRepository.Update(animalOwner);
            _logger.LogInformation("Animal owner updated successfully {@animalOwner}", updatedAnimalOwner);
            return Response<AnimalOwner>.SuccessResult(updatedAnimalOwner);
        }
        catch (Exception exception)
        {
            _logger.LogError(UpdateAnimalOwnerCommandFailed.EventId, exception, "{Code} {Message} {@animalOwner}", UpdateAnimalOwnerCommandFailed.Code, UpdateAnimalOwnerCommandFailed.Message, request);
            return Response<AnimalOwner>.ErrorResult(UpdateAnimalOwnerCommandFailed.Code, UpdateAnimalOwnerCommandFailed.Message);
        }
    }
}
