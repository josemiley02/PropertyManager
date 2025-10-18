using System;
using PropertyManagement.Domain.Entities.Identity;

namespace PropertyManagement.Domain.Entities;

public class Host : Entity<long>
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Property> Properties = new HashSet<Property>();
}
