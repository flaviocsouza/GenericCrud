namespace GenericCrud.Business;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(Guid id);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
}
