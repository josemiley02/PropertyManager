using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using PropertyManagement.Application.Abstractions.Authentication;
using PropertyManagement.Application.Abstractions.Identity.Models;
using PropertyManagement.Domain.Entities.Identity;
using PropertyManagement.Infrastructure.Abstractions;
namespace PropertyManagement.Application.Features.Auth.Login;
using User = PropertyManagement.Domain.Entities.Identity.AppUser;

public class LoginUserCommandHandler : CoreCommandHandler<LoginModel, LoginUserResponse>
{
    readonly ITokenHandler _tokenHandler;
    readonly UserManager<User> _userManager;
    readonly RoleManager<AppRole> _roleManager;
    public LoginUserCommandHandler(ITokenHandler tokenHandler, UserManager<User> userManager, RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _tokenHandler = tokenHandler;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public override async Task<LoginUserResponse> ExecuteAsync(LoginModel command, CancellationToken ct = default)
    {
        User? appUser = null;
        var userRepository = UnitOfWork!.DbRepository<User>();
        if (command.Email != null)
        {
            appUser = await userRepository.FirstAsync(filters: x => x.Email == command.Email);
        }
        if (appUser is null || appUser.StatusBaseEntity != Domain.Enums.StatusBaseEntity.Active || !(await _userManager.CheckPasswordAsync(appUser, command.Password)))
        {
            ThrowError("Invalid user or password");
        }
        var roles = await _userManager.GetRolesAsync(appUser);
        AccessToken accessToken = await _tokenHandler.CreateTokenAsync(appUser);
        return new LoginUserResponse
        {
            Id = appUser.Id,
            Email = appUser.Email!,
            Roles = roles,
            AccessToken = accessToken
        };
    }
}