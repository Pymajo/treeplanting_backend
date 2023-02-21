using netgo.treeplanting.Application.ViewModels;
using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.Interfaces
{
    public interface ISeedlingService
    {
        Task<List<SeedlingViewModel>> GetAllSeedlingsAsync();
        Task<SeedlingViewModel> CreateSeedlingAsync(Seedling seedling);
        Task<SeedlingViewModel> EditSeedlingAsync(Seedling seedling);
    }
}
