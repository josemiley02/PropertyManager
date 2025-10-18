using System;
using PropertyManagement.Domain.Enums;

namespace PropertyManagement.Domain.Entities.Identity;

public class Entity<T> where T : struct
{
    public T Id { get; set; }
}
public class Entity : IEntity
{
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public StatusBaseEntity StatusBaseEntity { get; set; }
}
