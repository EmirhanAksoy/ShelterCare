using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class DeleteShelterByIdCommandHandler : IRequestHandler<DeleteShelterByIdCommand, Response<bool>>
{
    private readonly IShelterRepository _shelterRepository;
    private readonly ILogger<GetShelterByIdQueryHandler> _logger;

    public DeleteShelterByIdCommandHandler(IShelterRepository shelterRepository, ILogger<GetShelterByIdQueryHandler> logger)
    {
        _shelterRepository = shelterRepository;
        _logger = logger;
    }
    public async Task<Response<bool>> Handle(DeleteShelterByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            DeleteShelterByIdCommandValidator validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {shelterId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return new Response<bool>().ErrorResult(ValidationError.Code, errorMessages);
            }
            Shelter shelter = await _shelterRepository.Get(request.Id);
            if (shelter is null)
            {
                _logger.LogError(ShelterNotFound.EventId, "{Code} {Message}  {shelterId}", ShelterNotFound.Code, ShelterNotFound.Message, request.Id.ToString());
                return new Response<bool>().ErrorResult(ShelterNotFound.Code, ShelterNotFound.Message);
            }
            bool isDeleted = await _shelterRepository.Delete(request.Id);
            if (isDeleted)
            {
                _logger.LogInformation("Shelter deleted successfully {shelterId}",request.Id.ToString());
            }
            return new Response<bool>().SuccessResult(isDeleted);
        }
        catch (Exception exception)
        {
            _logger.LogError(DeletehelterByIdCommandFailed.EventId, exception, "{Code} {Message} {shelterId}", DeletehelterByIdCommandFailed.Code, DeletehelterByIdCommandFailed.Message, request.Id.ToString());
            return new Response<bool>().ErrorResult(DeletehelterByIdCommandFailed.Code, DeletehelterByIdCommandFailed.Message);
        }
    }
}
