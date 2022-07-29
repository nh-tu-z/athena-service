using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Common.Events.Requests
{
    public class GetAllPipelineRequest 
    {
        public string? SearchableName { get; set; }
        public List<int?>? SecurityScoreLevel { get; set; }
        public List<int?>? CriticalityLevel { get; set; }
        public RangeDate? CreatedDateRange { get; set; }
        public PipelineSortableColumn SortColumn { get; set; } = PipelineSortableColumn.SecurityScore;
        public string SortDirection { get; set; } = SortDirections.Descending;
        public float Timezone { get; set; } = 0;
    }
}
