using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Common.Events.Requests
{
    public class GetAllAlertRequest
    {
        public string? SearchableName { get; set; }
        public List<int?>? Priority { get; set; }
        public List<int?>? Confidence { get; set; }
        public RangeDate? DetectedOnRange { get; set; }
        public List<int?>? Status { get; set; }
        public AlertSortableColumn SortColumn { get; set; } = AlertSortableColumn.Priority;
        public string SortDirection { get; set; } = SortDirections.Descending;
        public float Timezone { get; set; } = 0;
        public Guid? AlertRuleId { get; set; }
    }
}
