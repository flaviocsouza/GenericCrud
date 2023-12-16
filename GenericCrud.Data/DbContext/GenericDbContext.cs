using System.Reflection;
using GenericCrud.Business;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Data;

public class GenericDbContext : DbContext
{
  private readonly Assembly[] _assemblies;
  public GenericDbContext(DbContextOptions options, Assembly[] assemblies) : base(options) 
  {
    _assemblies  = assemblies;

  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenericDbContext).Assembly);

    foreach(var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
      relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

    var entities = AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(a => a.GetExportedTypes())
      .Where(c => 
        c.IsClass && 
        c.IsPublic &&
        !c.IsAbstract && 
        typeof(BaseEntity).IsAssignableFrom(c));

    foreach(var entity in entities)
      modelBuilder.Entity(entity);

    base.OnModelCreating(modelBuilder);
  }

}
