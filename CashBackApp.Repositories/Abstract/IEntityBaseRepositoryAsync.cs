using CashBackApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CashBackApp.Repositories.Abstract
{
    public interface IEntityBaseRepositoryAsync<T> where T : EntityBase
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetSingleAsync(Guid id);
        Task<T> GetSingleAsync(Guid id, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task AddAsync(List<T> entities);
        void Update(T entity);
        void Delete(Guid ID);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        Task CommitAsync();
    }
}
