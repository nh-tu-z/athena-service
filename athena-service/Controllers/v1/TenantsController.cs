using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Common.Events;
using AthenaService.Domain.ViewModels;

namespace AthenaService.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;
        //private readonly ITenantConnectionString _tenantConnectionString;
        public TenantsController(IMapper mapper, ITenantService tenantService)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _tenantService = tenantService
                ?? throw new ArgumentNullException(nameof(tenantService));
        }

        /// <summary>
        /// Get tenant by id async
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tenant = await _tenantService.GetTenantAsync(id);

            return Ok(new Response<TenantViewModel>(_mapper.Map<TenantViewModel>(tenant)));
        }
    }
}
