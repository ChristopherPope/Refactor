using FluentResults;
using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Results;
using GoodProductsApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GoodProductsApi.Controllers;

public abstract class ProductsApiController : ControllerBase
{
    protected ActionResult MakeResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return MakeErrorResult(result);
    }

    protected ActionResult MakeResult(Result result)
    {
        if (result.IsSuccess)
        {
            return Ok();
        }

        return MakeErrorResult(result);
    }

    protected ActionResult MakeCreateResult<T>(Result<T> result, string uri) where T : ProductsApiDto
    {
        if (result.IsSuccess)
        {
            return Created(uri, result.Value.Id);
        }

        return MakeErrorResult(result);
    }

    private ActionResult MakeErrorResult<T>(Result<T> result)
    {
        return MakeErrorResult(result);
    }

    private ActionResult MakeErrorResult(Result result)
    {
        if (result.HasError<EntityNotFoundError>())
        {
            return NotFound(result.GetErrorMessages());
        }

        return Problem(result.GetErrorMessages());
    }
}
