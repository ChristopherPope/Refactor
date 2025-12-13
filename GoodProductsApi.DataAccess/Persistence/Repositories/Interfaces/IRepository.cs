namespace GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> Create(T entity, CancellationToken cancellationToken);
    void Delete(T entity);
}
