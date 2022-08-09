using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Domain.ViewModels;

namespace AthenaService.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlertRulesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAlertRuleService _alertRuleService;

        public AlertRulesController(IMapper mapper,
            IAlertRuleService alertRuleService)
        {
            _mapper = mapper;
            _alertRuleService = alertRuleService;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var listAlertRule = await _alertRuleService.GetAllAsync();

            return Ok(_mapper.Map<List<AlertRuleViewModel>>(listAlertRule));
        }
    }
}
