using Dapper.Contrib.Extensions;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Admin.Entities
{
    [Table("TenantProvisionTask")]
    public class TenantProvisionTask
    {
        [ExplicitKey]
        public int TenantProvisionTaskId { get; set; }
        public int TenantId { get; set; }
        public TenantProvisionTaskState State { get; set; }
        public string ErrorMessage { get; set; } = String.Empty;
    }
}
