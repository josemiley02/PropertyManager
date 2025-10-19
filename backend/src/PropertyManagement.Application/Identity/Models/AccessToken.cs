using System;

namespace PropertyManagement.Application.Abstractions.Identity.Models;

public class AccessToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}