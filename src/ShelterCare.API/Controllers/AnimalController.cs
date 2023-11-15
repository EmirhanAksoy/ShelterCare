using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Mapper;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using System.Net;

namespace ShelterCare.API.Controllers;

[ApiController]
public class AnimalController : ControllerBase
{
    private readonly IMediator _mediator;
    public AnimalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(AnimalRoutes.GetAll)]
    public async Task<ActionResult<Response<List<Animal>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllAnimalQuery());
        return Ok(result);
    }

    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.NotFound)]
    [HttpGet(AnimalRoutes.Get)]
    public async Task<ActionResult<Response<Animal>>> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetAnimalByIdQuery(id));
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.BadRequest)]
    [HttpPost(AnimalRoutes.Create)]
    public async Task<ActionResult<Response<Animal>>> Create([FromBody] AnimalCreateRequest AnimalCreateRequest)
    {
        AnimalCreateRequestMapper animalCreateRequestMapper = new();
        var result = await _mediator.Send(animalCreateRequestMapper.CreateRequestToCommand(AnimalCreateRequest));
        if (result.ErrorCode == ValidationError.Code ||
            result.ErrorCode == AnimalConfirmationFailed.Code ||
            result.ErrorCode == OwnerOfAnimalConfirmationFailed.Code)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.NotFound)]
    [HttpPut(AnimalRoutes.Update)]
    public async Task<ActionResult<Response<Animal>>> Update([FromBody] AnimalUpdateRequest AnimalUpdateRequest)
    {
        AnimalUpdateRequestMapper animalUpdateRequestMapper = new();
        var result = await _mediator.Send(animalUpdateRequestMapper.UpdateRequestToCommand(AnimalUpdateRequest));
        if (result.ErrorCode == ValidationError.Code ||
            result.ErrorCode == AnimalConfirmationFailed.Code ||
            result.ErrorCode == OwnerOfAnimalConfirmationFailed.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Animal), (int)HttpStatusCode.NotFound)]
    [HttpDelete(AnimalRoutes.Delete)]
    public async Task<ActionResult<Response<bool>>> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteAnimalByIdCommand(id));
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}
