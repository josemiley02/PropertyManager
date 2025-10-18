using System;
using Microsoft.AspNetCore.Identity;
using PropertyManagement.Domain.Enums;
namespace PropertyManagement.Domain.Entities.Identity;

public class AppUser : IdentityUser<long>, IEntity
{
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public StatusBaseEntity StatusBaseEntity { get; set; }
}
