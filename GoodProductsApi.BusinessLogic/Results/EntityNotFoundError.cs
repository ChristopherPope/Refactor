using FluentResults;

namespace GoodProductsApi.BusinessLogic.Results;

public sealed class EntityNotFoundError : Error
{
    public EntityNotFoundError(string entityName, int id)
        : base($"{entityName} (ID: {id}) was not found")
    {
    }
}
