using Dapper.Contrib.Extensions;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Pipelines.Entities
{
    [Table("Alert")]
    public class Alert
    {
        [Key]
        public Guid AlertId { get; set; }
        public AlertPriority? Priority { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public AlertConfidence? Confidence { get; set; }
        public DateTime? DetectedOn { get; set; }
        public AlertStatus Status { get; set; }

        [Computed]
        public byte[]? Version { get; set; }
    }
}
