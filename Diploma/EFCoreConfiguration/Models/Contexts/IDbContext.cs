using Microsoft.EntityFrameworkCore;

namespace EFCoreConfiguration.Models.Contexts
{
    public interface IDbContext: IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
