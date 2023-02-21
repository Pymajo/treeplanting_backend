using Microsoft.EntityFrameworkCore;
using netgo.treeplanting.Domain.Core.Entities;
using netgo.treeplanting.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void AddRange(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> GetAllNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);

            entity.Delete();
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities, bool hardDelete = false)
        {
            if (hardDelete)
            {
                _dbSet.RemoveRange(entities);
                return;
            }

            foreach (var entity in entities)
            {
                entity.Delete();
            }
        }
    }
}
