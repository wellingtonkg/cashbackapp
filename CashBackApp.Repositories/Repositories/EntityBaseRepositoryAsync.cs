using CashBackApp.Database.Context;
using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CashBackApp.Repositories.Repositories
{
    public class EntityBaseRepositoryAsync<T> : IEntityBaseRepositoryAsync<T> where T : EntityBase, new()
    {
        private Context _context;
        
        #region Constructor
        public EntityBaseRepositoryAsync(Context context)
        {
            _context = context;
        }
        #endregion

        #region Selects
        /// <summary>
        /// Get all records of Entity
        /// </summary>
        /// <returns></returns>
        public async virtual Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity</returns>
        public async Task<T> GetSingleAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        
        /// <summary>
        /// Get an entity by id and set parent class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties">Name of related classes</param>
        /// <returns></returns>
        public async Task<T> GetSingleAsync(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get a list of Entities by especif searchs 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// Get a list of entities by especif search and set parent class
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate);
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async virtual Task AddAsync(T entity)
        {
            if (entity.IsValid())
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                await _context.Set<T>().AddAsync(entity);
            }
        }

        /// <summary>
        /// Add a list of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async virtual Task AddAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        /// <summary>
        /// Make an update in a entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// Delete an entity by Id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            var objectToDelete = new T { Id = id };
            _context.Attach<T>(objectToDelete);
            _context.Remove(objectToDelete);
        }

        /// <summary>
        /// Delete an entity object 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Delete a list of entities by a especific search
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in _context.Set<T>().Where(predicate))
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        public async virtual Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
