using System.Security.Claims;
using AthenaService.Interfaces;
using AthenaService.Domain.Models;

using AthenaService.Domain.Base;

namespace AthenaService.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUserModel GetCurrentUser()
        {
            var currentUserModel = new CurrentUserModel();
            if (_httpContextAccessor.HttpContext?.User is ClaimsPrincipal principal)
            {
                var claims = principal.Claims;
                if (null != claims)
                {
                    currentUserModel.Ver = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.Ver)?.Value;
                    _ = Int32.TryParse(claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.UserId)?.Value, out int userId);
                    currentUserModel.UserId = userId;
                    _ = Int32.TryParse(claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.TenantId)?.Value, out int currentTenantId);
                    currentUserModel.CurrentTenantId = currentTenantId;
                    currentUserModel.GivenName = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.GivenName)?.Value;
                    currentUserModel.FamilyName = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.FamilyName)?.Value;
                    currentUserModel.FirstName = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.FirstName)?.Value;
                    currentUserModel.LastName = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.LastName)?.Value;
                    currentUserModel.Email = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.Email)?.Value;
                    currentUserModel.Name = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.Name)?.Value;
                    var jsonString = claims.FirstOrDefault(x => x.Type == CommonConstants.Claims.Roles)?.Value;
                    currentUserModel.Roles = jsonString != null ? new List<string>() { jsonString } : new List<string>();
                }
            }
            return currentUserModel;
        }

        public string? GetClaimByType(string type)
        {
            string? claimValue = null;
            try
            {
                if (_httpContextAccessor.HttpContext?.User is ClaimsPrincipal principal)
                {
                    var claims = principal.Claims;
                    if (null != claims)
                    {
                        claimValue = claims.FirstOrDefault(x => x.Type == type)?.Value;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return claimValue;
        }
    }
}
