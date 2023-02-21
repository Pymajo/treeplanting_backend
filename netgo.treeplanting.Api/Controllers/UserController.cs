using Microsoft.AspNetCore.Mvc;
using netgo.treeplanting.Api.Controllers;
using MediatR;
using netgo.treeplanting.Application.Interfaces;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Application.ViewModels;
using Microsoft.AspNetCore.Cors;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : TreePlantingController
{
    private readonly IUserService _userService;
    private readonly IUserSystemRepository _userRepository;

    public UserController(
        INotificationHandler<DomainNotification> notifications,
        IUserSystemRepository userRepository,
        IMediatorHandler mediator,
        IUserService userService) : base(notifications, mediator)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUserAsync(User user)
    {
        await _userService.CreateUserAsync(user);
        return Response(user);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAsync()
    {
        var config = await _userService.GetAllUsersAsync();

        return Response(config);
    }
    
    [HttpPut("users")]
    public async Task<IActionResult> UpdateUserAsync(
        [FromBody] UserViewModel config)
    {
        await _userService.UpdateUserAsync(config);
        return Response(_userService);
    }

    [HttpPatch("users/{id}")]
    public async Task<IActionResult> ConfirmUserAsync(Guid id)
    {
        await _userService.ConfirmUserAsync(id);
        return Response(_userService);
    }

    [HttpPatch("PromoteAdmin/{id}")]
    public async Task<IActionResult> PromoteAdminAsync(Guid id)
    {
        await _userService.PromoteAdminAsync(id);
        return Response(_userService);
    }

    [HttpPatch("DemoteAdmin/{id}")]
    public async Task<IActionResult> DemoteAdminAsync(Guid id)
    {
        await _userService.DemoteAdminAsync(id);
        return Response(_userService);
    }

    [HttpPatch("PromoteTreecoinsDeterminer/{id}")]
    public async Task<IActionResult> PromoteTreecoinsDeterminerAsync(Guid id)
    {
        await _userService.PromoteTreecoinsDeterminerAsync(id);
        return Response(_userService);
    }

    [HttpPatch("DemoteTreecoinsDeterminer/{id}")]
    public async Task<IActionResult> DemoteTreecoinsDeterminerAsync(Guid id)
    {
        await _userService.DemoteTreecoinsDeterminerAsync(id);
        return Response(_userService);
    }

    [HttpPatch("PromotePlantingOfficer/{id}")]
    public async Task<IActionResult> PromotePlantingOfficerAsync(Guid id)
    {
        await _userService.PromotePlantingOfficerAsync(id);
        return Response(_userService);
    }

    [HttpPatch("DemotePlantingOfficer/{id}")]
    public async Task<IActionResult> DemotePlantingOfficerAsync(Guid id)
    {
        await _userService.DemotePlantingOfficerAsync(id);
        return Response(_userService);
    }

    [HttpPatch("PromotePollManager/{id}")]
    public async Task<IActionResult> PromotePollManagerAsync(Guid id)
    {
        await _userService.PromotePollManagerAsync(id);
        return Response(_userService);
    }

    [HttpPatch("DemotePollManager/{id}")]
    public async Task<IActionResult> DemotePollManagerAsync(Guid id)
    {
        await _userService.DemotePollManagerAsync(id);
        return Response(_userService);
    }

    [HttpPatch("PromoteSeedlingsManager/{id}")]
    public async Task<IActionResult> PromoteSeedlingsManagerAsync(Guid id)
    {
        await _userService.PromoteSeedlingsManagerAsync(id);
        return Response(_userService);
    }

    [HttpPatch("DemoteSeedlingsManager/{id}")]
    public async Task<IActionResult> DemoteSeedlingsManagerAsync(Guid id)
    {
        await _userService.DemoteSeedlingsManagerAsync(id);
        return Response(_userService);
    }

    [HttpDelete("users")]
    public async Task<IActionResult> DeleteUserAsync([FromBody] Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return Response(id);
    }

    [HttpPost("loginUser")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginModel loginData)
    {
        var response = await _userService.LoginUserAsync(loginData);

        if (response == null || response.Token == String.Empty)
        {
            return BadRequest();
        }

        return Ok(response);
    }

    [HttpGet("Me/{id}")]
    public async Task<IActionResult> GetMe(Guid id)
    {
        var me = await _userService.GetMe(id);
        return Response(me);
    }
}
