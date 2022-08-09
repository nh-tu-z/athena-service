using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class TenantProvisionTaskViewModel
    {
        public int TenantProvisionTaskId { get; set; }
        public TenantProvisionTaskState State { get; set; }
        public string ErrorMessage { get; set; } = String.Empty;
    }
}
