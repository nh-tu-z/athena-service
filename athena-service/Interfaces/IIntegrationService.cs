using AthenaService.Domain.Settings.Entities;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Interfaces
{
    public interface IIntegrationService
    {
        Task<Integration> GetByIdAsync(Guid id);

        Task<int> UpdateStateByTokenIdAsync(Guid tokenId, IntegrationState state);
    }
}
