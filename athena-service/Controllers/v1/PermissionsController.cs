using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;
using AthenaService.Common.Events;
using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public PermissionsController(IMapper mapper, IPermissionService permissionService, ICurrentUser currentUser)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _permissionService = permissionService
                ?? throw new ArgumentNullException(nameof(permissionService));
            _currentUser = currentUser
                ?? throw new ArgumentNullException(nameof(currentUser));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([Required][FromQuery(Name = "type")] PermissionType type)
        {
            IEnumerable<PermissionModel> permissions = await _permissionService.GetAllByTypeAsync(type);
            IEnumerable<PermissionViewModel> permissionViewModels =
                _mapper.Map<IEnumerable<PermissionViewModel>>(permissions);
            return Ok(new Response<IEnumerable<PermissionViewModel>>(permissionViewModels));
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAllAdminAsync()
        {
            return await GetAllByTypeAsync(PermissionType.Admin);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllUserAsync()
        {
            return await GetAllByTypeAsync(PermissionType.User);
        }

        private async Task<IActionResult> GetAllByTypeAsync(PermissionType type)
        {
            IEnumerable<PermissionModel> permissions = await _permissionService.GetAllByTypeAsync(type);
            IEnumerable<PermissionViewModel> permissionViewModels =
                _mapper.Map<IEnumerable<PermissionViewModel>>(permissions);
            return Ok(new Response<IEnumerable<PermissionViewModel>>(permissionViewModels));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyPermissionAsync()
        {
            var userId = _currentUser.GetCurrentUser().UserId;
            var roleTypes = (_currentUser.GetCurrentUser().Roles ?? new List<string>()).Select(x => int.Parse(x)).ToArray();
            var isSuccessParse = int.TryParse(_currentUser.GetClaimByType(CommonConstants.Clams.TenantIdClaimName), out int tenantId);
            var myPermissions = await _permissionService.GetPermissionByUserId(userId, roleTypes, isSuccessParse ? tenantId : null);
            var myPermissionsViewModels = _mapper.Map<IEnumerable<UserPermissionViewModel>>(myPermissions);
            return Ok(new Response<IEnumerable<UserPermissionViewModel>>(myPermissionsViewModels));
        }
    }
}
