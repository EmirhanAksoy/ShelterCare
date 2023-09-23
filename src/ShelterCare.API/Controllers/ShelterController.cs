using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Contracts.Responses;
using ShelterCare.API.Routes;
using ShelterCare.Application;

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
    public async Task<ActionResult<Response<List<ShelterResponse>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllSheltersQuery()).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet(ShelterRoutes.Get)]
    public ActionResult<ShelterResponse> Get([FromRoute]string id)
    {
        return Ok(new ShelterResponse());
    }

    [HttpPost(ShelterRoutes.Create)]
    public ActionResult<ShelterResponse> Create([FromBody] ShelterCreateRequest shelterCreateRequest)
    {
        return Ok(new ShelterResponse());
    }

    [HttpPost(ShelterRoutes.Update)]
    public ActionResult<ShelterResponse> Update([FromRoute]string id,[FromBody] ShelterUpdateRequest shelterUpdateRequest)
    {
        return Ok(new ShelterResponse());
    }

    [HttpDelete(ShelterRoutes.Delete)]
    public ActionResult<bool> Delete([FromRoute] string id)
    {
        return Ok(true);
    }
}
