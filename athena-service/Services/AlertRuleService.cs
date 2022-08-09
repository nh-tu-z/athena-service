using AthenaService.Interfaces;
using AthenaService.Domain.Models;
using AthenaService.Persistence;
using AthenaService.Common.CommandText;

namespace AthenaService.Services
{
    public class AlertRuleService : IAlertRuleService
    {
        private readonly IPersistenceService _persistenceService;

        public AlertRuleService(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
        }

        public async Task<IList<AlertRuleModel>> GetAllAsync()
        {
            var listAlertRule = await _persistenceService.QueryAsync<AlertRuleModel>(CommandAlertText.GetAll);

            return listAlertRule.ToList();
        }
    }
}
