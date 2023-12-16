using GenericCrud.Business;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Data;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
{
  protected readonly GenericDbContext _context;
  protected readonly DbSet<TEntity> _dbSet;
  public BaseRepository(GenericDbContext context)
  {
    _context = context;
    _dbSet = context.Set<TEntity>();
  }

  public async Task<IEnumerable<TEntity>> GetAll() => await _dbSet.AsNoTracking().ToListAsync();

  public async Task<TEntity> GetById(Guid id) => await _dbSet.AsNoTracking().FirstAsync(e => e.Id == id && e.IsActive);

  public async Task Create(TEntity entity)
  {
    await _dbSet.AddAsync(entity);
    await SaveChanges();
  }

  public async Task Update(TEntity entity)
  {
    _dbSet.Update(entity);
    await SaveChanges();
  }

  public async Task Delete(Guid id)
  {
    _dbSet.Remove(new TEntity { Id = id });
    await SaveChanges();
  }
  protected async Task SaveChanges()
  {
    await _context.SaveChangesAsync();
  }

  public void Dispose()
  {
    _context?.Dispose();
  }
}
