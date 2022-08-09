using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Common.Events;
using AthenaService.Common.Events.Requests;
using AthenaService.Domain.ViewModels;

namespace AthenaService.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAlertService _alertService;

        public AlertsController(IMapper mapper,
            IAlertService alertService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] AlertStatusInputModel status)
        {
            var result = await _alertService.UpdateStatusAsync(id, status.NewStatus, status.CurrentStatus);
            return result ? NoContent() : BadRequest();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllAlertRequest request)
        {
            var listAlert = await _alertService.GetAllAsync(request);
            var mapped = _mapper.Map<List<AlertViewModel>>(listAlert);

            // hardcode
            return Ok(new PagedResponse<List<AlertViewModel>>(mapped, 1, 1, 1));
        }
    }
}
