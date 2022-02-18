using PCS.Core.CoreEntities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PCS.Core.Repositories.Abstract.GenericRepositories
{
    public interface IGenericRepository<TEntity, TPk> where TEntity : HardDeletableEntity<TPk>
    {
        void Add(TEntity entity);
        void AddList(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteList(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateList(IEnumerable<TEntity> entities);
        Task<TEntity> GetByIdAsync(TPk id, params Expression<Func<TEntity, object>>[] navigationExpressions);
        Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] navigationExpressions);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetListByExpression(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] navigationExpressions);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

    }
}
