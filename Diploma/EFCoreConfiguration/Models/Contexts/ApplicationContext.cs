using EFCoreConfiguration.Models.Contexts.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EFCoreConfiguration.Models.Contexts
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RolesConfiguration());
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new UsersWithRoleConfiguration());
        }
    }
}
