using System;
using Microsoft.IdentityModel.Tokens;

namespace PropertyManagement.Infrastructure.Crypto;

public class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
    }
}