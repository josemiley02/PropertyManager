using System;
using Microsoft.AspNetCore.Identity;
using PropertyManagement.Domain.Enums;

namespace PropertyManagement.Domain.Entities.Identity;

public class AppRole : IdentityRole<long>, IEntity
{
    public bool IsLock { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public StatusBaseEntity StatusBaseEntity { get; set; }
}
