using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Mapper.ShelterMapper;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;
using System.Net;

namespace ShelterCare.API.Controllers;

[ApiController]
public class ShelterController : ControllerBase
{
    private readonly IMediator _mediator;
    public ShelterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(ShelterRoutes.GetAll)]
    public async Task<ActionResult<Response<List<Shelter>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllSheltersQuery()).ConfigureAwait(false);
        return Ok(result);
    }

    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.NotFound)]
    [HttpGet(ShelterRoutes.Get)]
    public async Task<ActionResult<Shelter>> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetShelterByIdQuery(id)).ConfigureAwait(false);
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == ShelterNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.BadRequest)]
    [HttpPost(ShelterRoutes.Create)]
    public async Task<ActionResult<Response<Shelter>>> Create([FromBody] ShelterCreateRequest shelterCreateRequest)
    {
        ShelterCreateRequestMapper shelterCreateRequestMapper = new();
        var result = await _mediator.Send(shelterCreateRequestMapper.CreateRequestToCommand(shelterCreateRequest)).ConfigureAwait(false);
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.NotFound)]
    [HttpPut(ShelterRoutes.Update)]
    public async Task<ActionResult<Shelter>> Update([FromBody] ShelterUpdateRequest shelterUpdateRequest)
    {
        ShelterUpdateRequestMapper shelterUpdateRequestMapper = new();
        var result = await _mediator.Send(shelterUpdateRequestMapper.UpdateRequestToCommand(shelterUpdateRequest)).ConfigureAwait(false);
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == ShelterNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Shelter), (int)HttpStatusCode.NotFound)]
    [HttpDelete(ShelterRoutes.Delete)]
    public async Task<ActionResult<Response<bool>>> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteShelterByIdCommand(id)).ConfigureAwait(false);
        if (result.ErrorCode == ValidationError.Code)
        {
            return BadRequest(result);
        }
        if (result.ErrorCode == ShelterNotFound.Code)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}
