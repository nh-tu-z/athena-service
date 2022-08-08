using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class TenantViewModel
    {
        public int TenantId { get; set; }
        public string TenantAlias { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public TenantState State { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastActivity { get; set; } = string.Empty;
        public DateTime? LastActivityDate { get; set; }
    }
}
