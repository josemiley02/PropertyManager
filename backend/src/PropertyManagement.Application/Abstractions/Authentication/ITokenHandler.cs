using System;
using System.Security.Claims;
using PropertyManagement.Application.Abstractions.Identity.Models;
using PropertyManagement.Domain.Entities.Identity;

namespace PropertyManagement.Application.Abstractions.Authentication;

public interface ITokenHandler
{
    Task<AccessToken> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}