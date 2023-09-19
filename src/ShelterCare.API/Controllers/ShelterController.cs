using Microsoft.AspNetCore.Mvc;
using ShelterCare.API.Contracts.Requests;
using ShelterCare.API.Contracts.Responses;
using ShelterCare.API.Routes;

namespace ShelterCare.API.Controllers;

[ApiController]
public class ShelterController : ControllerBase
{
    [HttpGet(ShelterRoutes.GetAll)]
    public ActionResult<List<ShelterResponse>> GetAll()
    {
        return Ok(new List<ShelterResponse>());
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
