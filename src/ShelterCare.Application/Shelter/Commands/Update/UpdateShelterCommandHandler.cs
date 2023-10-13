using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class UpdateShelterCommandHandler : IRequestHandler<UpdateShelterCommand,Response<Shelter>>
{
    private readonly IShelterRepository _shelterRepository;
    private readonly ILogger<UpdateShelterCommandHandler> _logger;

    public UpdateShelterCommandHandler(IShelterRepository shelterRepository, ILogger<UpdateShelterCommandHandler> logger)
    {
        _shelterRepository = shelterRepository;
        _logger = logger;
    }

    public async Task<Response<Shelter>> Handle(UpdateShelterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            UpdateShelterCommandValidation validationRules = new();
            var validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages}", ValidationError.Code, ValidationError.Message, errorMessages);
                return Response<Shelter>.ErrorResult(ValidationError.Code, errorMessages);
            }

            Shelter existingShelter = await _shelterRepository.Get(request.Id);
            if (existingShelter is null)
            {
                _logger.LogError(ShelterNotFound.EventId, "{Code} {Message}  {shelterId}", ShelterNotFound.Code, ShelterNotFound.Message, request.Id.ToString());
                return Response<Shelter>.ErrorResult(ShelterNotFound.Code, ShelterNotFound.Message);
            }

            Shelter shelter = new()
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                OwnerFullName = request.OwnerFullName,
                FoundationDate = request.FoundationDate,
                TotalAreaInSquareMeters = request.TotalAreaInSquareMeters,
                Website = request.Website
            };
            Shelter updatedShelter = await _shelterRepository.Update(shelter);
            _logger.LogInformation("Shelter updated successfully {@shelter}", updatedShelter);
            return Response<Shelter>.SuccessResult(updatedShelter);
        }
        catch (Exception exception)
        {
            _logger.LogError(UpdateShelterCommandFailed.EventId, exception, "{Code} {Message} {@shelter}", UpdateShelterCommandFailed.Code, UpdateShelterCommandFailed.Message, request);
            return Response<Shelter>.ErrorResult(UpdateShelterCommandFailed.Code, UpdateShelterCommandFailed.Message);
        }
    }
}
