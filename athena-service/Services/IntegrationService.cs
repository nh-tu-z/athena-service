using AthenaService.Interfaces;
using AthenaService.Domain.Settings.Entities;
using AthenaService.Persistence;
using AthenaService.Common.CommandText;

namespace AthenaService.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IPersistenceService _persistenceService;

        public IntegrationService(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        public async Task<Integration> GetByIdAsync(Guid id) =>
            await _persistenceService.QuerySingleOrDefaultAsync<Integration>(CommandIntegrationText.GetById, new { id });
    }
}
