using Microsoft.EntityFrameworkCore;
using PCS.Core.CoreEntities.Abstract;
using PCS.Core.Repositories.Abstract.GenericRepositories;
using PCS.Database.Context.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PCS.Repository.Repositories.Concrete.GenericRepositories
{
    public class HardDeletableRepository<TEntity, TPk> : IGenericRepository<TEntity, TPk>
        where TEntity : HardDeletableEntity<TPk>, new()
    {
        private readonly PcsDbContext dbContext;

        public HardDeletableRepository(PcsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public virtual void AddList(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().AddRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual void DeleteList(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public virtual void UpdateList(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().UpdateRange(entities);
        }
        public virtual async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] navigationExpressions)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>().Where(expression);
            if (navigationExpressions.Any())
            {
                foreach (var exp in navigationExpressions)
                {
                    query = query.Include(exp);
                }
            }
            return await query.SingleOrDefaultAsync(expression);
        }

        public virtual async Task<TEntity> GetByIdAsync(TPk id, params Expression<Func<TEntity, object>>[] navigationExpressions)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            if (navigationExpressions.Any())
            {
                foreach (var exp in navigationExpressions)
                {
                    query = query.Include(exp);
                }
            }
            return await query.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual IQueryable<TEntity> GetListByExpression(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] navigationExpressions)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>().Where(expression);
            if (navigationExpressions.Any())
            {
                foreach (var exp in navigationExpressions)
                {
                    query = query.Include(exp);
                }
            }
            return query;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dbContext.Set<TEntity>().AnyAsync(expression);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            return expression is null ? await query.CountAsync() : await query.CountAsync(expression);
        }

    }
}
