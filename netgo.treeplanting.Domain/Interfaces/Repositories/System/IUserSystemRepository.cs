using Microsoft.AspNetCore.Identity;
using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Interfaces.Repositories.System
{
    public interface IUserSystemRepository : IRepository<User>
    {
        Task<User> GetUserAsync(Guid id);
       
    }
}
