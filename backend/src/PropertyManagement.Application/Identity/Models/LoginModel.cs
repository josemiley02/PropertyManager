using PropertyManagement.Application.Features.Auth.Login;

public class LoginModel : ICommand<LoginUserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}