using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using netgo.treeplanting.Domain.Interfaces;
using netgo.treeplanting.Infrastructure.Database;

namespace netgo.treeplanting.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(dbUpdateException, "An error occured during commiting changes");
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
