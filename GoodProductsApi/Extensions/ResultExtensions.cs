using FluentResults;

namespace GoodProductsApi.Extensions;

public static class ResultExtensions
{
    static public string GetErrorMessages<T>(this Result<T> result)
    {
        return string.Join(", ", result.Errors.Select(x => x.Message));
    }
}
