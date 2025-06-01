using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Configuration;

namespace Soat.Eleven.FastFood.Api.Configuration;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult SendReponse(ResultResponse result)
    {
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    protected IActionResult SendGetResponse(ResultResponse result)
    {
        if (result.Data is null)
            return NotFound(result);

        return Ok(result);
    }
}
