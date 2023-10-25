using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class DeleteAnimalOwnerByIdCommandHandler : IRequestHandler<DeleteAnimalOwnerByIdCommand, Response<bool>>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    private readonly ILogger<DeleteAnimalOwnerByIdCommandHandler> _logger;

    public DeleteAnimalOwnerByIdCommandHandler(ILogger<DeleteAnimalOwnerByIdCommandHandler> logger, IAnimalOwnerRepository animalOwnerRepository)
    {
        _logger = logger;
        _animalOwnerRepository = animalOwnerRepository;
    }

    public async Task<Response<bool>> Handle(DeleteAnimalOwnerByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            DeleteAnimalOwnerByIdCommandValidation validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {animalOwnerId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return Response<bool>.ErrorResult(ValidationError.Code, errorMessages);
            }
            AnimalOwner animalOwner = await _animalOwnerRepository.Get(request.Id);
            if (animalOwner is null)
            {
                _logger.LogError(AnimalOwnerNotFound.EventId, "{Code} {Message}  {animalOwnerId}", AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message, request.Id.ToString());
                return Response<bool>.ErrorResult(AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message);
            }
            bool isDeleted = await _animalOwnerRepository.Delete(request.Id);
            if (isDeleted)
            {
                _logger.LogInformation("Animal owner deleted successfully {animalOwnerId}", request.Id.ToString());
            }
            return Response<bool>.SuccessResult(isDeleted);
        }
        catch (Exception exception)
        {
            _logger.LogError(DeleteAnimalOwnerByIdCommandFailed.EventId, exception, "{Code} {Message} {animalOwnerId}", DeleteAnimalOwnerByIdCommandFailed.Code, DeleteAnimalOwnerByIdCommandFailed.Message, request.Id.ToString());
            return Response<bool>.ErrorResult(DeleteAnimalOwnerByIdCommandFailed.Code, DeleteAnimalOwnerByIdCommandFailed.Message);
        }
    }
}
