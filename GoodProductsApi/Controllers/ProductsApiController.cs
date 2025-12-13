using FluentResults;
using GoodProductsApi.BusinessLogic.Results;
using GoodProductsApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GoodProductsApi.Controllers;

public abstract class ProductsApiController : ControllerBase
{
    protected ActionResult MakeActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        if (result.HasError<EntityNotFoundError>())
        {
            return NotFound(result.GetErrorMessages());
        }

        return Problem(result.GetErrorMessages());
    }
}
