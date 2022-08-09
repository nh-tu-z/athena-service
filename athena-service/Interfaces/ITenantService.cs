using AthenaService.Domain.Admin.Entities;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Interfaces
{
    public interface ITenantService
    {
        Task<Tenant> GetByIdAsync(int id);
        Task<TenantModel> GetTenantAsync(int id);
        Task<TenantModel> CreateTenantAsync(SaveTenantViewModel tenant);
        Task<TenantModel> UpdateTenantStateAsync(int id, TenantState state);
    }
}
