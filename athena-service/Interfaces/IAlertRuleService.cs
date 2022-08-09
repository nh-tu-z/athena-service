using AthenaService.Domain.Models;

namespace AthenaService.Interfaces
{
    public interface IAlertRuleService
    {
        Task<IList<AlertRuleModel>> GetAllAsync();
    }
}
