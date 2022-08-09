using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class AlertViewModel
    {
        public Guid AlertId { get; set; }
        public AlertPriority? Priority { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AlertConfidence? Confidence { get; set; }
        public DateTime? DetectedOn { get; set; }
        public AlertStatus Status { get; set; } = AlertStatus.New;
    }
}
