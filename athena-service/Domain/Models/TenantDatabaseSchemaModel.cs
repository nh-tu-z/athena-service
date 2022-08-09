using AthenaService.Domain.Admin.Entities;

namespace AthenaService.Domain.Models
{
    public class TenantDatabaseSchemaModel : TenantDatabaseSchema
    {
        public Tenant? Tenant { get; set; } 
    }
}
