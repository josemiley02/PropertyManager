using System;
using PropertyManagement.Domain.Enums;

namespace PropertyManagement.Domain.Entities.Identity;

public interface IEntity
{
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public StatusBaseEntity StatusBaseEntity { get; set; }
}
