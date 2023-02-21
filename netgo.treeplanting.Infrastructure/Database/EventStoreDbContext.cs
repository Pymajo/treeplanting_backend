using Microsoft.EntityFrameworkCore;
using netgo.treeplanting.Domain.Core.DomainEvents;
using netgo.treeplanting.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Database
{
    public class EventStoreDbContext : DbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
        {
        }

        public virtual DbSet<StoredDomainEvent> StoredDomainEvents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredDomainEventConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
