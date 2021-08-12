using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Diploma.Models.Contexts
{
    public class ApplicationContext: IdentityDbContext<User>, IDbContext
    {
        public override DbSet<TEntity> Set<TEntity>() where TEntity : class =>
            (DbSet<TEntity>)((IDbSetCache)this).GetOrAddSet(this.GetDependencies().SetSource, typeof(TEntity));

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
