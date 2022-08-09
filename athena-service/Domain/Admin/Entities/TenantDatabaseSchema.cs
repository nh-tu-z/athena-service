using Dapper.Contrib.Extensions;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Admin.Entities
{
    [Table("TenantDatabaseSchema")]
    public class TenantDatabaseSchema
    {
        [ExplicitKey]
        public int TenantDatabaseSchemaId { get; set; }
        public int TenantId { get; set; }
        public string DatabaseName { get; set; } = String.Empty;
        public string SchemaName { get; set; } = String.Empty;
        public TenantDatabaseSchemaState State { get; set; }
    }
}
