using Dapper.Contrib.Extensions;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Admin.Entities
{
    [Table("Permission")]
    public class Permission
    {
        [ExplicitKey]
        public int PermissionId { get; set; }
        public PermissionType Type { get; set; }
        public string Module { get; set; } = string.Empty;
        public string Function { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PermissionCode { get; set; } = string.Empty;
    }
}
