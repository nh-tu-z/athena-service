using Dapper.Contrib.Extensions;
using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Pipelines.Entities
{
    [Table("Pipeline")]
    public class Pipeline : BaseEntity
    {
        [Key]
        public Guid PipelineId { get; set; }
        public string PipelineName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SecurityScore { get; set; } = string.Empty;
        public int Assets { get; set; }
        public CriticalityEnums Criticality { get; set; }
        public StateEnums State { get; set; }
        public string LastActivity { get; set; } = string.Empty;
        public DateTime? LastActivityTimeStamp { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
