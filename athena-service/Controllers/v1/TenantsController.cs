using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AthenaService.Interfaces;

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
    }
}
