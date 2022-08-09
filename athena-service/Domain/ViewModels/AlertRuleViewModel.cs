using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class AlertRuleViewModel
    {
        public Guid AlertRuleId { get; set; }
        public string RuleName { get; set; } = string.Empty;
        public AlertPriority? Priority { get; set; }
        public DateTime? CreatedOn { get; set; }
        public AlertRuleStatus Status { get; set; } = AlertRuleStatus.RunRunning;
    }
}
