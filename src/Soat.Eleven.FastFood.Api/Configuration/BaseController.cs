using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Configuration;

namespace Soat.Eleven.FastFood.Api.Configuration;

public abstract class BaseController : ControllerBase
{
    protected IActionResult SendReponse(ResultResponse result)
    {
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
}
