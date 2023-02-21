using Microsoft.EntityFrameworkCore;
using netgo.treeplanting.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Repository
{
    public class SystemRepository<TEntity> : BaseRepository<TEntity> where TEntity : Entity
    {
        public SystemRepository(DbContext context) : base(context) 
        {

        }
    }
}
