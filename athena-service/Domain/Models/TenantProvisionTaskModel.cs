using AthenaService.Domain.Admin.Entities;

namespace AthenaService.Domain.Models
{
    public class TenantProvisionTaskModel : TenantProvisionTask
    {
        public Tenant? Tenant { get; set; }
    }
}
