using System;
using PropertyManagement.Application.Abstractions.Identity.Models;

namespace PropertyManagement.Application.Features.Auth.Login;

public record LoginUserResponse
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = new HashSet<string>();
    public AccessToken AccessToken { get; set; } = null!;
}