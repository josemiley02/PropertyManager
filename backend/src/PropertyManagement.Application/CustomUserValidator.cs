using System;
using Microsoft.AspNetCore.Identity;
using PropertyManagement.Domain.Entities.Identity;

namespace PropertyManagement.Application;

public class CustomUserValidator<TUser> : UserValidator<TUser> where TUser : AppUser
{
    public async override Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
    {
        var result = await base.ValidateAsync(manager, user);

        if (string.IsNullOrWhiteSpace(user.UserName))
        {
            return IdentityResult.Success;
        }

        return result;
    }
}