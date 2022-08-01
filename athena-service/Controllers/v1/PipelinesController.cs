using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib;
using AthenaService.Interfaces;
using AthenaService.Logger;
using AthenaService.Scratch;
using AthenaService.Domain.ViewModels;

namespace AthenaService.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

            //CreatePipelineDetailInputModel model = new CreatePipelineDetailInputModel();

            //Guid pipelineId = await _pipelineService.CreatePipelineAsync(model);

            //return Ok($"pipeline id: {pipelineId}");

            return Ok("Hello tuhngo@kms-technology.com");
        }

        [HttpGet("{id}/check")]
        public async Task<IActionResult> CheckAsync([FromRoute(Name = "id")] Guid id)
        {
            _logManager.Information($"id in api/pipelines/{{id}}/check: {id}", "TODO");
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CheckConnectionAsync([FromRoute(Name = "id")] Guid id)
        {
            _logManager.Information($"id in api/pipelines/{{id}}: {id}", "TODO");
            return NoContent();
        }
    }
}
