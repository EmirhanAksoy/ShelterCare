﻿using MediatR;
using Microsoft.Extensions.Logging;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;

public class GetAllSheltersQueryHandler : IRequestHandler<GetAllSheltersQuery, Response<List<Shelter>>>
{

    private readonly IShelterRepository _shelterRepository;
    private readonly ILogger<GetAllSheltersQueryHandler> _logger;

    public GetAllSheltersQueryHandler(IShelterRepository shelterRepository, ILogger<GetAllSheltersQueryHandler> logger)
    {
        _shelterRepository = shelterRepository;
        _logger = logger;
    }

    public async Task<Response<List<Shelter>>> Handle(GetAllSheltersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Shelters retrieved successfully");
            return Response<List<Shelter>>.SuccessResult(await _shelterRepository.GetAll());
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAllSheltersQueryFailed.EventId, exception, GetAllSheltersQueryFailed.Code);
            return Response<List<Shelter>>.ErrorResult(GetAllSheltersQueryFailed.Code, GetAllSheltersQueryFailed.Message);
        }
    }
}
