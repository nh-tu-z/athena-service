using Microsoft.AspNetCore.Mvc;
using AthenaService.Interfaces;
using AthenaService.Logger;

namespace AthenaService.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class PipelinesController : ControllerBase
    {
        private readonly IPipelineService _pipelineService;
        private readonly ILogManager _logManager;

        public PipelinesController(IPipelineService pipelineService, ILogManager logManager)
        {
            _pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }

        [HttpGet]
        public async Task<IActionResult> GetPipelineListAsync()
        {
            _logManager.Information("I am in controller");
            return Ok("Hello world");
        }
    }
}
