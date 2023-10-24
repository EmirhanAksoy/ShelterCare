using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Mapper.AnimalSpecieMapper;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using System.Net;

namespace ShelterCare.API.Controllers;

[ApiController]
public class AnimalSpecieController : ControllerBase
{
    private readonly IMediator _mediator;
    public AnimalSpecieController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(Response<List<AnimalSpecie>>), (int)HttpStatusCode.OK)]
    [HttpGet(AnimalSpecieRoutes.GetAll)]
    public async Task<ActionResult<Response<List<AnimalSpecie>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllAnimalSpeciesQuery());
        return Ok(result);
    }

    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.NotFound)]
    [HttpGet(AnimalSpecieRoutes.Get)]
    public async Task<ActionResult<Response<AnimalSpecie>>> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetAnimaSpecieByIdQuery(id));
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalSpecieNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.BadRequest)]
    [HttpPost(AnimalSpecieRoutes.Create)]
    public async Task<ActionResult<Response<AnimalSpecie>>> Create([FromBody] AnimalSpecieCreateRequest animalSpecieCreateRequest)
    {
        AnimalSpecieCreateRequestMapper animalSpecieCreateRequestMapper = new();
        var result = await _mediator.Send(animalSpecieCreateRequestMapper.CreateRequestToCommand(animalSpecieCreateRequest));
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response<AnimalSpecie>), (int)HttpStatusCode.NotFound)]
    [HttpDelete(AnimalSpecieRoutes.Delete)]
    public async Task<ActionResult<Response<bool>>> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteAnimalSpecieByIdCommand(id));
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalSpecieNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}
