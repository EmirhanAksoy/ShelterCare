using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Mapper.ShelterMapper;
using ShelterCare.API.Routes;
using ShelterCare.Application;
using ShelterCare.Core.Domain;

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

    [HttpGet(ShelterRoutes.Get)]
    public async Task<ActionResult<Shelter>> Get([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetShelterByIdQuery(id)).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost(ShelterRoutes.Create)]
    public async Task<ActionResult<Response<Shelter>>> Create([FromBody] ShelterCreateRequest shelterCreateRequest)
    {
        ShelterCreateRequestMapper shelterCreateRequestMapper = new();
        var result = await _mediator.Send(shelterCreateRequestMapper.CreateRequestToCommand(shelterCreateRequest)).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost(ShelterRoutes.Update)]
    public ActionResult<Shelter> Update([FromRoute] string id, [FromBody] ShelterUpdateRequest shelterUpdateRequest)
    {
        return Ok(new Shelter());
    }

    [HttpDelete(ShelterRoutes.Delete)]
    public async Task<ActionResult<Response<bool>>> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteShelterByIdCommand(id)).ConfigureAwait(false);
        return Ok(result);
    }
}
