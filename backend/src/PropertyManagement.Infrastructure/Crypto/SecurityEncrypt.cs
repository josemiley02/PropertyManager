using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PropertyManagement.Infrastructure.Crypto;

public class SecurityEncrypt
{
    public static SecurityKey CreateSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}