using AthenaService.Domain.Settings.Entities;

namespace AthenaService.Interfaces
{
    public interface IIntegrationService
    {
        Task<Integration> GetByIdAsync(Guid id);
    }
}
