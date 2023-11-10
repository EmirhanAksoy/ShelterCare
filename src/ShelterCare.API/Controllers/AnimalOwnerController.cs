using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using System.Net;
using ShelterCare.API.Mapper.AnimalOwner;
namespace AnimalOwnerCare.API.Controllers;

[ApiController]
public class AnimalOwnerController : ControllerBase
{
    private readonly IMediator _mediator;
    public AnimalOwnerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(AnimalOwnerRoutes.GetAll)]
    public async Task<ActionResult<Response<List<AnimalOwner>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllAnimalOwnerQuery());
        return Ok(result);
    }

    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.NotFound)]
    [HttpGet(AnimalOwnerRoutes.Get)]
    public async Task<ActionResult<Response<AnimalOwner>>> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetAnimalOwnerByIdQuery(id));
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalOwnerNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.BadRequest)]
    [HttpPost(AnimalOwnerRoutes.Create)]
    public async Task<ActionResult<Response<AnimalOwner>>> Create([FromBody] AnimalOwnerCreateRequest AnimalOwnerCreateRequest)
    {
        AnimalOwnerCreateRequestMapper animalOwnerCreateRequestMapper = new();
        var result = await _mediator.Send(animalOwnerCreateRequestMapper.CreateRequestToCommand(AnimalOwnerCreateRequest));
        if (result.ErrorCode == ValidationError.Code ||
            result.ErrorCode == AnimalOwnerConfirmationFailed.Code)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.NotFound)]
    [HttpPut(AnimalOwnerRoutes.Update)]
    public async Task<ActionResult<Response<AnimalOwner>>> Update([FromBody] AnimalOwnerUpdateRequest AnimalOwnerUpdateRequest)
    {
        AnimalOwnerUpdateRequestMapper animalOwnerUpdateRequestMapper = new();
        var result = await _mediator.Send(animalOwnerUpdateRequestMapper.UpdateRequestToCommand(AnimalOwnerUpdateRequest));
        if (result.ErrorCode == ValidationError.Code ||
            result.ErrorCode == AnimalOwnerConfirmationFailed.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalOwnerNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AnimalOwner), (int)HttpStatusCode.NotFound)]
    [HttpDelete(AnimalOwnerRoutes.Delete)]
    public async Task<ActionResult<Response<bool>>> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteAnimalOwnerByIdCommand(id));
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == AnimalOwnerNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}
