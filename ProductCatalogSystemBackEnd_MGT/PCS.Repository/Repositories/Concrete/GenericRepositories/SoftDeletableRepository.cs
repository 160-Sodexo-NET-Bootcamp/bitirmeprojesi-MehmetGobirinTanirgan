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
    public class SoftDeletableRepository<TEntity, TPk> : HardDeletableRepository<TEntity, TPk>, IGenericRepository<TEntity, TPk>
        where TEntity : SoftDeletableEntity<TPk>, new()
    {
        private readonly PcsDbContext dbContext;

        public SoftDeletableRepository(PcsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            dbContext.Set<TEntity>().Update(entity);
        }

        public override void DeleteList(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(x => x.IsDeleted = true);
            dbContext.Set<TEntity>().UpdateRange(entities);
        }

        public override async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] navigationExpressions)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>().Where(x => !x.IsDeleted).Where(expression);
            if (navigationExpressions.Any())
            {
                foreach (var exp in navigationExpressions)
                {
                    query = query.Include(exp);
                }
            }
            return await query.SingleOrDefaultAsync(expression);
        }

        public override async Task<TEntity> GetByIdAsync(TPk id, params Expression<Func<TEntity, object>>[] navigationExpressions)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            if (navigationExpressions.Any())
            {
                foreach (var exp in navigationExpressions)
                {
                    query = query.Include(exp);
                }
            }
            return await query.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id.Equals(id));
        }

        public override async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().Where(x => !x.IsDeleted).AsNoTracking().ToListAsync();
        }

        public override IQueryable<TEntity> GetListByExpression(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] navigationExpressions)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>().Where(x => !x.IsDeleted).Where(expression);

            if (navigationExpressions.Any())
            {
                foreach (var exp in navigationExpressions)
                {
                    query = query.Include(exp);
                }
            }
            return query;
        }

        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dbContext.Set<TEntity>().AnyAsync(expression);
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            var query = dbContext.Set<TEntity>().Where(x => !x.IsDeleted);
            return expression is null ? await query.CountAsync() : await query.CountAsync(expression);
        }

    }
}
