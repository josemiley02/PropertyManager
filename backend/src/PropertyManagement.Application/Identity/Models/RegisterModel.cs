using System;
using PropertyManagement.Common.Dto;

namespace PropertyManagement.Application.Identity.Models;

public class RegisterModel : ICommand<Response<NoContentData>>
{
    public required string Email { get; set; }
    public string? Username { get; set; }
    public required string Password { get; set; }
    public IEnumerable<string> Roles { get; set; } = new HashSet<string>();
}