using System.Data;
using Dapper;
using AthenaService.Interfaces;
using AthenaService.Domain.Models;
using AthenaService.Common.Events.Requests;
using AthenaService.Persistence;
using AthenaService.Domain.Pipelines.Entities;
using AthenaService.Common.CommandText;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Services
{
    public class AlertService : IAlertService
    {
        private readonly IPersistenceService _persistenceService;
        private static readonly Dictionary<AlertStatus, AlertStatus[]> _updateStatuses = new Dictionary<AlertStatus, AlertStatus[]>
        {
            { AlertStatus.New, new AlertStatus[] { AlertStatus.Dismissed, AlertStatus.Resolved, AlertStatus.Active, AlertStatus.Escalated } },
            { AlertStatus.Active, new AlertStatus[] { AlertStatus.New, AlertStatus.Dismissed, AlertStatus.Resolved, AlertStatus.Escalated } },
            { AlertStatus.Dismissed, new AlertStatus[] { AlertStatus.New, AlertStatus.Resolved, AlertStatus.Escalated } },
            { AlertStatus.Resolved, new AlertStatus[] { AlertStatus.New, AlertStatus.Dismissed, AlertStatus.Escalated } },
        };

        public AlertService(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
        }

        public async Task<bool> UpdateStatusAsync(Guid id, AlertStatus newStatus, AlertStatus currentStatus)
        {
            var alert = await GetAlertByIdAsync(id);
            ValidateStatus(newStatus, alert, currentStatus);

            var result = await _persistenceService.ExecuteAsync(CommandAlertText.UpdateAlertStatus, new { alert.AlertId, alert.Version, Status = newStatus });
            return result > 0;
        }

        private async Task<Alert> GetAlertByIdAsync(Guid id)
            => await _persistenceService.QuerySingleOrDefaultAsync<Alert>(CommandAlertText.GetAlertById, new { id });

        private void ValidateStatus(AlertStatus newStatus, Alert alert, AlertStatus currentStatus)
        {
            if (alert == null)
            {
                //throw new NotFoundException(AlertMessage.AlertNotFound);
            }

            if (alert.Status == AlertStatus.Escalated)
            {
                //throw new ValidationException(AlertMessage.AlertEscalated);
            }

            if (currentStatus != alert.Status)
            {
                //throw new ValidationException(AlertMessage.AlertStateChanged);
            }

            if (!CanUpdateAlertStatus(alert.Status, newStatus))
            {
                //throw new ValidationException(AlertMessage.InvalidStateChange);
            }
        }

        private bool CanUpdateAlertStatus(AlertStatus currentStatus, AlertStatus newStatus)
        {
            if (_updateStatuses.ContainsKey(currentStatus))
            {
                var statuses = _updateStatuses[currentStatus];
                return statuses.Contains(newStatus);
            }
            return false;
        }

        public async Task<IList<AlertModel>> GetAllAsync(GetAllAlertRequest request)
        {
            var timezoneHours = (int)request.Timezone;
            var timezoneMinutes = (int)((request.Timezone - timezoneHours) * 60);

            var parameters = new DynamicParameters();

            parameters.Add("SearchValue", request.SearchableName, DbType.String);
            parameters.Add("IsFilteredByPriority", request.Priority?.Count > 0, DbType.Boolean);
            parameters.Add("Priority", (request.Priority ?? new List<int?>()).Select(id => id).ToArray());
            parameters.Add("IsFilteredByConfidence", request.Confidence?.Count > 0, DbType.Boolean);
            parameters.Add("Confidence", (request.Confidence ?? new List<int?>()).Select(id => id).ToArray());
            parameters.Add("DetectedOnRange", request.DetectedOnRange, DbType.Int32);
            parameters.Add("IsFilteredByStatus", request.Status?.Count > 0, DbType.Boolean);
            parameters.Add("Status", (request.Status ?? new List<int?>()).Select(id => id).ToArray());
            parameters.Add("AlertRuleId", request.AlertRuleId);



            parameters.Add("TimezoneHours", timezoneHours, DbType.Int32);
            parameters.Add("TimezoneMinutes", timezoneMinutes, DbType.Int32);

            //parameters.Add("PageNo", request.PageNumber, DbType.Int32);
            //parameters.Add("PageSize", request.PageSize, DbType.Int32);
            parameters.Add("SortColumn", request.SortColumn, DbType.Int32);
            parameters.Add("SortDirection", request.SortDirection, DbType.String);

            var listAlert = await _persistenceService.QueryAsync<AlertModel>(CommandAlertText.GetAll, parameters);

            return listAlert.ToList();
        }
    }
}
