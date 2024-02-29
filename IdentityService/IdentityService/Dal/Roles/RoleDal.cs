using Core.Dal.Base;
using Core.Dal.Entities;

namespace Dal.Roles;

public record RoleDal : BaseEntity<Guid>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<PermissionsEnum> Permissions { get; set; }
}