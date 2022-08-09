using System.Text.RegularExpressions;
using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Persistence;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;
using AthenaService.Domain.Admin.Entities;
using AthenaService.Common.CommandText.Tenant;
using AthenaService.Domain.Base;

using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Services
{
    public class TenantService : ITenantService
    {
        private readonly IMapper _mapper;
        private readonly IPersistenceService _adminPersistenceService;
        private readonly ICurrentUser _currentUser;

        public TenantService(IMapper mapper, IPersistenceService persistenceService, ICurrentUser currentUser)
        {
            _mapper = mapper;
            _adminPersistenceService = persistenceService;
            _currentUser = currentUser;
        }

        public async Task<Tenant> GetByIdAsync(int id) =>
            await _adminPersistenceService.QuerySingleOrDefaultAsync<Tenant>(CommandTenantText.GetById, new { id });

        public async Task<TenantModel> GetTenantAsync(int tenantId)
        {
            var tenants = await _adminPersistenceService.QueryAsync<TenantModel, User, TenantModel>(CommandTenantText.GetTenant, (t, u) =>
            {
                t.CreatedBy = u.UserId;
                t.CreatedByUser = u;
                return t;
            },
            new { TenantId = tenantId },
            splitOn: nameof(User.UserId));

            return tenants.FirstOrDefault();
        }

        public async Task<TenantModel> CreateTenantAsync(SaveTenantViewModel tenant)
        {
            var entity = _mapper.Map<Tenant>(tenant);

            if (await CheckTenantNameIsExistedAsync(entity.Name))
            {
                // throw error
            }

            entity.TenantAlias = await GenerateTenantAliasAsync(entity.Name);
            entity.State = TenantState.AwaitingSetup;
            entity.CreatedAt = DateTime.UtcNow;
            entity.LastActivity = Activities.TenantActivities.Created;
            entity.LastActivityDate = DateTime.UtcNow;
            //entity.CreatedBy = _currentUser.GetCurrentUser().UserId;
            // hardcode userid
            entity.CreatedBy = 1;

            var id = await _adminPersistenceService.InsertAsync(entity);

            return await GetTenantAsync(id);
        }

        public async Task<TenantModel> UpdateTenantStateAsync(int id, TenantState state)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                // throw 
            }
        }

        private async Task<bool> CheckTenantNameIsExistedAsync(string tenantName, int? tenantId = null)
        {
            var tenantNameIsExisted = await _adminPersistenceService.QuerySingleOrDefaultAsync<dynamic>(CommandTenantText.CheckExistedTenantName, new { Name = tenantName, TenantId = tenantId }) != null;

            return tenantNameIsExisted;
        }

        private async Task<string> GenerateTenantAliasAsync(string tenantName)
        {
            var removedDenyCharacter = Regex.Replace(tenantName.Trim(), @"[^a-z0-9\s]", " ", RegexOptions.IgnoreCase).Trim().ToLower();
            var removedRedundantSpace = Regex.Replace(removedDenyCharacter, @"[ ]{2,}", " ");
            var truncated = removedRedundantSpace.Substring(0, removedRedundantSpace.Length >= CommonConstants.TenantAliasMaxLenght ? CommonConstants.TenantAliasMaxLenght : removedRedundantSpace.Length);
            var tenantAlias = truncated.Trim().Replace(' ', '-');
            tenantAlias = !string.IsNullOrEmpty(tenantAlias) ? tenantAlias : CommonConstants.DefaultTenantAlias;
            return await GetValidTenantAliasAsync(tenantAlias);
        }

        private async Task<string> GetValidTenantAliasAsync(string tenantAlias)
        {
            var validTenantAlias = false;
            var tenantAliasNumber = 1;
            var tenantAliasToCheck = tenantAlias;

            while (!validTenantAlias)
            {
                validTenantAlias = await _adminPersistenceService.QuerySingleOrDefaultAsync<dynamic>(CommandTenantText.CheckExistedTenantAlias, new { TenantAlias = tenantAliasToCheck }) == null;
                if (validTenantAlias)
                {
                    return tenantAliasToCheck;
                }
                tenantAliasToCheck = (tenantAlias.Length + tenantAliasNumber.ToString().Length > CommonConstants.TenantAliasMaxLenght ? tenantAlias.Substring(0, CommonConstants.TenantAliasMaxLenght - tenantAliasNumber.ToString().Length) : tenantAlias) + tenantAliasNumber;
                tenantAliasNumber++;
            }

            throw new Exception(TenantMessages.TenantAliasIsNotGenerated);
        }
    }
}
