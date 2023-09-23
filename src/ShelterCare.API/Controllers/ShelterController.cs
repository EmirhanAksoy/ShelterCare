using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Contracts.Responses;
using ShelterCare.API.Routes;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.API.Controllers;

[ApiController]
public class ShelterController : ControllerBase
{

    private readonly IShelterRepository _shelterRepository;
    public ShelterController(IShelterRepository shelterRepository)
    {
        _shelterRepository = shelterRepository;
    }


    [HttpGet(ShelterRoutes.GetAll)]
    public async Task<ActionResult<List<ShelterResponse>>> GetAll()
    {
        var result = await _shelterRepository.GetAll().ConfigureAwait(false);
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
