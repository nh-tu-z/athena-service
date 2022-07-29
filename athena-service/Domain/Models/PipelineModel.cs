using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Models
{
    public class PipelineModel
    {
        public Guid PipelineId { get; set; }
        public string PipelineName { get; set; } = string.Empty;
        public int SecurityScore { get; set; }
        public int Assets { get; set; }
        public CriticalityEnums Criticality { get; set; }
        public StateEnums State { get; set; }
        public string LastActivity { get; set; } = string.Empty;
        public DateTime? LastActivityTimeStamp { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
