using Dapper.Contrib.Extensions;
using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Admin.Entities
{
    [Table("Tenant")]
    public class Tenant : FullBaseEntity
    {
        [Key]
        public int TenantId { get; set; }
        public string TenantAlias { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public TenantState State { get; set; }
    }
}
