using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using netgo.treeplanting.Application.ViewModels;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(UserViewModel config);
        Task<List<UserViewModel>> GetAllUsersAsync();
        Task DeleteUserAsync(Guid id);
        Task ConfirmUserAsync(Guid id);
        Task PromoteAdminAsync(Guid id);
        Task DemoteAdminAsync(Guid id);
        Task PromoteTreecoinsDeterminerAsync(Guid id);
        Task DemoteTreecoinsDeterminerAsync(Guid id);
        Task PromotePlantingOfficerAsync(Guid id);
        Task DemotePlantingOfficerAsync(Guid id);
        Task PromotePollManagerAsync(Guid id);
        Task DemotePollManagerAsync(Guid id);
        Task PromoteSeedlingsManagerAsync(Guid id);
        Task DemoteSeedlingsManagerAsync(Guid id);
        Task<AuthenticatedResponse> LoginUserAsync(LoginModel loginData);
        Task<MeViewModel> GetMe(Guid id);
        Task<TreecoinsViewModel> AddTreecoins(Guid id, int deposit);
        Task<TreecoinsViewModel> RemoveTreecoins(Guid id, int withdraw);
    }
}
