using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Domain.ViewModels;
using AthenaService.Domain.Models;
using AthenaService.Common.Events;

namespace AthenaService.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TenantProvisionTaskController : ControllerBase
    {
        private readonly ITenantProvisionTaskService _tenantProvisionTaskService;
        private readonly IMapper _mapper;

        public TenantProvisionTaskController(
            ITenantProvisionTaskService tenantProvisionTaskService,
            IMapper mapper)
        {
            _tenantProvisionTaskService = tenantProvisionTaskService
                ?? throw new ArgumentNullException(nameof(tenantProvisionTaskService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateTenantProvisionTaskViewModel createTenantProvisionTaskViewModel)
        {
            try
            {
                TenantProvisionTaskModel? tenantProvisionTaskModel
                    = await _tenantProvisionTaskService.CreateAsync(createTenantProvisionTaskViewModel.TenantId);
                return Ok(new Response<TenantProvisionTaskViewModel>(
                    _mapper.Map<TenantProvisionTaskViewModel>(tenantProvisionTaskModel)));
            }
            // TODO - customize exception 
            catch (Exception exception)
            {
                // throw 
                return BadRequest(exception.Message);
            }
        }
    }
}
