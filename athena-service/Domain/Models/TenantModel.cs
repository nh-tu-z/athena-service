using AthenaService.Domain.Admin.Entities;

namespace AthenaService.Domain.Models
{
    public class TenantModel : Tenant
    {
        public User? CreatedByUser { get; set; }
    }
}
