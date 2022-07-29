using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Persistence;
using AthenaService.Common.CommandText.Pipeline;
using AthenaService.Common.Events.Requests;
using AthenaService.Domain.Pipelines.Entities;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;

namespace AthenaService.Services
{
    public class PipelineService : IPipelineService
    {
        private readonly IMapper _mapper;
        private readonly IPersistenceService _persistenceService;

        public PipelineService(IMapper mapper, IPersistenceService persistenceService)
        {
            _mapper = mapper;
            _persistenceService = persistenceService;
        }

        public async Task<Pipeline> GetPipelineByIdAsync(Guid id) =>
            await _persistenceService.QuerySingleOrDefaultAsync<Pipeline>(CommandPipelineText.GetById, new { id });

        public async Task<IList<PipelineModel>> GetAllAsync(GetAllPipelineRequest request)
        {
            return new List<PipelineModel>();
        }

        public async Task<Guid> CreatePipelineAsync(CreatePipelineDetailInputModel createPipelineInputModel)
        {
            return new Guid();
        }

        public async Task<EditPipelineSettingsModel> EditPipelineDetailsAsync(Guid pipelineId, EditPipelineSettingsInputModel editPipelineInputModel)
        {
            return new EditPipelineSettingsModel();
        }
    }
}
