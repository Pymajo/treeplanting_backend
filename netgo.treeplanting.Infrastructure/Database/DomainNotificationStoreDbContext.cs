using Microsoft.EntityFrameworkCore;
using netgo.treeplanting.Domain.Core.DomainNotifications;
using netgo.treeplanting.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Database
{
    public class DomainNotificationStoreDbContext : DbContext
    {
        public DomainNotificationStoreDbContext(DbContextOptions<DomainNotificationStoreDbContext> options) : base(options)
        {
        }

        public virtual DbSet<StoredDomainNotification> StoredDomainNotifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DomainNotificationStoreConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
