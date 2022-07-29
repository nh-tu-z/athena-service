using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Models
{
    public class EditPipelineSettingsModel
    {
        public Guid PipelineId { get; set; }
        public string PipelineName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CriticalityEnums Criticality { get; set; }
    }
}
