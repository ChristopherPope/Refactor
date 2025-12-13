using FluentResults;
using GoodProductsApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GoodProductsApi.Controllers;

public abstract class ProductsApiController : ControllerBase
{
    protected ActionResult ReturnResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return Problem(result.GetErrorMessages());
    }
}
