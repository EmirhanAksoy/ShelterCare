using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class GetAllAnimalSpeciesQueryHandler : IRequestHandler<GetAllAnimalSpeciesQuery, Response<List<AnimalSpecie>>>
{
    private readonly IAnimalSpecieRepository _animalSpecieRepository;
    private readonly ILogger<GetAllAnimalSpeciesQueryHandler> _logger;

    public GetAllAnimalSpeciesQueryHandler(IAnimalSpecieRepository animalSpecieRepository, ILogger<GetAllAnimalSpeciesQueryHandler> logger)
    {
        _animalSpecieRepository = animalSpecieRepository;
        _logger = logger;
    }
    public async Task<Response<List<AnimalSpecie>>> Handle(GetAllAnimalSpeciesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Response<List<AnimalSpecie>>.SuccessResult(await _animalSpecieRepository.GetAll());
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAllAnimalSpeciesQueryFailed.EventId, exception, "{Code} {Message}", GetAllAnimalSpeciesQueryFailed.Code, GetAllAnimalSpeciesQueryFailed.Message);
            return Response<List<AnimalSpecie>>.ErrorResult(GetAllAnimalSpeciesQueryFailed.Code, GetAllAnimalSpeciesQueryFailed.Message);
        }
    }
}
