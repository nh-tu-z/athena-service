using Dapper.Contrib.Extensions;
using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Settings.Entities
{
    [Table("Integration")]
    public class Integration : BaseEntity
    {
        [Key]
        public Guid IntegrationId { get; set; }
        public string IntegrationName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EnvironmentType Environment { get; set; }
        public Guid TokenId { get; set; }
        public IntegrationState State { get; set; }
        public DateTime? StateUpdatedAt { get; set; }

        [Computed]
        public byte[]? Version { get; set; }
    }
}
