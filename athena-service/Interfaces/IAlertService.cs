using AthenaService.Domain.Models;
using AthenaService.Common.Events.Requests;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Interfaces
{
    public interface IAlertService
    {
        Task<bool> UpdateStatusAsync(Guid id, AlertStatus newStatus, AlertStatus currentStatus);
        Task<IList<AlertModel>> GetAllAsync(GetAllAlertRequest request);
    }
}
