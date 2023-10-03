using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Contracts.Responses;
using ShelterCare.API.Mapper.ShelterMapper;
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
    public async Task<ActionResult<Response<List<ShelterCreatedResponse>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllSheltersQuery()).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet(ShelterRoutes.Get)]
    public ActionResult<ShelterCreatedResponse> Get([FromRoute]string id)
    {
        return Ok(new ShelterCreatedResponse());
    }

    [HttpPost(ShelterRoutes.Create)]
    public async Task<ActionResult<Response<ShelterCreatedResponse>>> Create([FromBody] ShelterCreateRequest shelterCreateRequest)
    {
        ShelterCreateRequestMapper shelterCreateRequestMapper = new();
        var result = await _mediator.Send(shelterCreateRequestMapper.CreateRequestToCommand(shelterCreateRequest)).ConfigureAwait(false);
        ShelterCreatedResponseMapper shelterCreatedResponseMapper = new();
        var response = Response<ShelterCreatedResponse>.SuccessResult(shelterCreatedResponseMapper.ShelterToShelterCreatedResponse(result.Data));
        return Ok(response);
    }

    [HttpPost(ShelterRoutes.Update)]
    public ActionResult<ShelterCreatedResponse> Update([FromRoute]string id,[FromBody] ShelterUpdateRequest shelterUpdateRequest)
    {
        return Ok(new ShelterCreatedResponse());
    }

    [HttpDelete(ShelterRoutes.Delete)]
    public ActionResult<bool> Delete([FromRoute] string id)
    {
        return Ok(true);
    }
}
