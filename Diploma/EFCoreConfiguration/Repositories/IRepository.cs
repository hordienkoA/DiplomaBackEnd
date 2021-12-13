using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFCoreConfiguration.Models;

namespace EFCoreConfiguration.Repositories
{
    interface IRepository<TEntity> where TEntity: BaseEntity
    {
        IQueryable<TEntity> Source { get; }
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<TEntity> FindAsync(int id);
        Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
    }
}
