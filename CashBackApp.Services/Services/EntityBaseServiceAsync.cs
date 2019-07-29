using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CashBackApp.Services.Services
{
    public class EntityBaseServiceAsync<T> : IEntityBaseServiceAsync<T> where T : EntityBase, new()
    {

        private IEntityBaseRepositoryAsync<T> _repostory;

        #region Constructor
        public EntityBaseServiceAsync(IEntityBaseRepositoryAsync<T> repostory)
        {
            _repostory = repostory;
        }
        #endregion

        public async virtual Task<List<T>> GetAllAsync()
        {
            return await _repostory.GetAllAsync();
        }

        public async virtual Task<T> GetSingleAsync(Guid id)
        {
            return await _repostory.GetSingleAsync(id);
        }
               
        public async Task<T> GetSingleAsync(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await _repostory.GetSingleAsync(id, includeProperties);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _repostory.FindBy(predicate);
        }
               
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return _repostory.FindBy(predicate, includeProperties);
        }

        public async virtual Task AddAsync(T entity)
        {
            await _repostory.AddAsync(entity);
        }

        public async virtual Task AddAsync(List<T> entities)
        {
            await _repostory.AddAsync(entities);
        }

        public virtual void Update(T entity)
        {
            _repostory.Update(entity);
        }

        public void Delete(Guid id)
        {
            _repostory.Delete(id);
        }

        public virtual void Delete(T entity)
        {
            _repostory.Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
           _repostory.Delete(predicate);
        }

        public async virtual Task CommitAsync()
        {
            await _repostory.CommitAsync();
        }
    }
}
