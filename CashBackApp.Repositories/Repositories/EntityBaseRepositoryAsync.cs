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

        public async virtual Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetSingleAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
               
        public async Task<T> GetSingleAsync(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
               
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate);
        }

        public async virtual Task AddAsync(T entity)
        {
            if (entity.IsValid())
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                await _context.Set<T>().AddAsync(entity);
            }
        }

        public async virtual Task AddAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var objectToDelete = new T { Id = id };
            _context.Attach<T>(objectToDelete);
            _context.Remove(objectToDelete);
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in _context.Set<T>().Where(predicate))
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public async virtual Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
