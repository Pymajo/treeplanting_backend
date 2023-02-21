

namespace netgo.treeplanting.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void AddRange(params TEntity[] entities);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllNoTracking();

        Task<TEntity> GetByIdAsync(Guid id);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task RemoveAsync(Guid id);

        void Update(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities, bool hardDelete = false);
    }
}
