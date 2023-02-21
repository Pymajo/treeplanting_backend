using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using netgo.treeplanting.Application.Interfaces;
using netgo.treeplanting.Application.ViewModels;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Commands;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Errors;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using Newtonsoft.Json.Linq;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediatorHandler _bus;
        private readonly IUserSystemRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityMessageService _emailService;
        private readonly IConfiguration _config;
        public UserService(IMediatorHandler bus, IUserSystemRepository userRepository, IMapper mapper, IIdentityMessageService emailService, IConfiguration config)
        {
            _bus = bus;
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
            _config = config;
        }

        public async Task CreateUserAsync(User user)
        {
            var userDto = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == user.Id || x.Email == user.Email);

            if (userDto == null || userDto.Id != user.Id && userDto.Email != user.Email)
            {
                var passwordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                                    .ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash)));

                var cmd = new CreateUserCommand(
                                    user.UserName,
                                    passwordHash,
                                    user.Email,
                                    user.EmailRegistered,
                                    user.IsAdmin,
                                    user.TreecoinsDeterminer,
                                    user.PlantingOfficer,
                                    user.PollManager,
                                    user.SeedlingsManager,
                                    user.Treecoins,
                                    Guid.NewGuid());

                await _bus.SendCommandAsync(cmd);

                var identityMessage = new IdentityMessage();
                identityMessage.Destination = user.Email;
                identityMessage.Subject = "netgo Treeplanting Register Confirmation";
                identityMessage.Body = $"<p><h1>Welcome on netgo Treeplanting<h1></p>" +
                    $"<p><h3>Click on Confirm now to Confirm your email</h3></p>" +
                    $"<a href=\"http://localhost:4200/confirmRegister/{cmd.Id}\">Confirm now!</a>";
                await _emailService.SendAsync(identityMessage);
            }
            else
            {
                await _bus.RaiseEventAsync(new DomainNotification("Commit",
                "Benutzer existiert bereits, Bitte erneut versuchen.",
                DomainErrorCodes.CommitError));
                return;
            }

        }
        public async Task UpdateUserAsync(UserViewModel configuration)
        {
            var cmd = new UpdateUserCommand(
                configuration.UserName,
                configuration.Email,
                configuration.Id);

            await _bus.SendCommandAsync(cmd);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var command = new DeleteUserCommand(id);
            await _bus.SendCommandAsync(command);
        }
        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAll().ToListAsync();
            var usersViewModels = _mapper.Map<List<User>, List<UserViewModel>>(users);

            return usersViewModels;
        }

        public async Task ConfirmUserAsync(Guid id)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            var cmd = new EmailRegisterCommand(
                user.EmailRegistered = true,
                id);

            await _bus.SendCommandAsync(cmd);
        }

        public async Task PromoteAdminAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new PromoteAdminCommand(id);

            await _bus.SendCommandAsync(cmd);

        }

        public async Task DemoteAdminAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new DemoteAdminCommand(id);

            await _bus.SendCommandAsync(cmd);
        }

        public async Task PromoteTreecoinsDeterminerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new PromoteTreecoinsDeterminerCommand(id);

            await _bus.SendCommandAsync(cmd);


        }
        public async Task DemoteTreecoinsDeterminerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new DemoteTreecoinsDeterminerCommand(id);

            await _bus.SendCommandAsync(cmd);
        }

        public async Task PromotePlantingOfficerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new PromotePlantingOfficerCommand(id);

            await _bus.SendCommandAsync(cmd);
        }
        public async Task DemotePlantingOfficerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new DemotePlantingOfficerCommand(id);

            await _bus.SendCommandAsync(cmd);
        }

        public async Task PromotePollManagerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new PromotePollManagerCommand(id);

            await _bus.SendCommandAsync(cmd);
        }

        public async Task DemotePollManagerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new DemotePollManagerCommand(id);

            await _bus.SendCommandAsync(cmd);

        }

        public async Task PromoteSeedlingsManagerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new PromoteSeedlingsManagerCommand(id);

            await _bus.SendCommandAsync(cmd);
        }

        public async Task DemoteSeedlingsManagerAsync(Guid id)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new DemoteSeedlingsManagerCommand(id);

            await _bus.SendCommandAsync(cmd);

        }

        public async Task<AuthenticatedResponse> LoginUserAsync(LoginModel loginData)
        {
            var loginDataPw = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                                    .ComputeHash(Encoding.UTF8.GetBytes(loginData.PasswordHash)));

            var user = await _userRepository.GetAll().FirstOrDefaultAsync
                (user => user.Email == loginData.Email &&
                user.PasswordHash == loginDataPw &&
                user.EmailRegistered == true);

            if (user.Email == loginData.Email && user.PasswordHash == loginDataPw && user.EmailRegistered == true)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["IssuerSigningKey:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                var response = new AuthenticatedResponse { Token = tokenString, Id = user.Id };
                return response;
            }

            return new AuthenticatedResponse { Token = "" }; ;
        }

        public async Task<MeViewModel> GetMe(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var me = new MeViewModel(
                user.Id,
                user.UserName,
                user.Email,
                user.IsAdmin,
                user.TreecoinsDeterminer,
                user.PlantingOfficer,
                user.PollManager,
                user.SeedlingsManager,
                user.Treecoins);

            return me;
        }

        public async Task<TreecoinsViewModel> AddTreecoins(Guid id, int deposit)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new AddTreecoinsCommand(id, deposit);

            await _bus.SendCommandAsync(cmd);
            var response = new TreecoinsViewModel(
                user.Id,
                cmd.Treecoins);

            return response;
            
        }

        public async Task<TreecoinsViewModel> RemoveTreecoins(Guid id, int withdraw)
        {
            var users = await _userRepository.GetAll().ToArrayAsync();
            var user = users.FirstOrDefault(x => x.Id == id);

            var cmd = new RemoveTreecoinsCommand(id, withdraw);

            await _bus.SendCommandAsync(cmd);
            var response = new TreecoinsViewModel(
                user.Id,
                cmd.Treecoins);

            return response;
        }
    }
}
