using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;
using System.Net.Http.Json;

namespace ShelterCare.Application;

public class CreateAnimalOwnerCommandHandler : IRequestHandler<CreateAnimalOwnerCommand, Response<AnimalOwner>>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    private readonly ILogger<CreateAnimalOwnerCommandHandler> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateAnimalOwnerCommandHandler(ILogger<CreateAnimalOwnerCommandHandler> logger, IAnimalOwnerRepository animalOwnerRepository, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _animalOwnerRepository = animalOwnerRepository;
        _httpClientFactory = httpClientFactory;
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

            bool confirmationResult = await ConfirmOwner(request.NationalId);
            if (!confirmationResult)
            {
                _logger.LogError("{Code} {Message} {nationalId}", AnimalOwnerConfirmationFailed.Code, AnimalOwnerConfirmationFailed.Message, request.NationalId);
                return Response<AnimalOwner>.ErrorResult(AnimalOwnerConfirmationFailed.Code, AnimalOwnerConfirmationFailed.Message);
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

    public async Task<bool> ConfirmOwner(string nationalId)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(ApplicationConstants.ConfirmApi);
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"/national-id/confirm/{nationalId}");
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            NationalIdConfirmResponse response = await httpResponseMessage.Content.ReadFromJsonAsync<NationalIdConfirmResponse>();
            return response.Success;
        }
        return false;
    }
}
