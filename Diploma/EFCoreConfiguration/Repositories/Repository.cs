using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConfiguration.Repositories
{
    public class Repository<TContext, TEntity> :
        IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : IDbContext
    {
        protected readonly TContext Context;

        public Repository(TContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Source => Context.Set<TEntity>();
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().Where(expression).ToListAsync();
        }
    }
}
