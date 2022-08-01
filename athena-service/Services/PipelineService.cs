using Dapper;
using AutoMapper;
using AthenaService.Interfaces;
using AthenaService.Persistence;
using AthenaService.Common.CommandText.Pipeline;
using AthenaService.Common.Events.Requests;
using AthenaService.Domain.Pipelines.Entities;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;
using static AthenaService.Domain.Base.Enums;

using AthenaService.Scratch;

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
            var now = DateTime.UtcNow;
            var parameters = new DynamicParameters();

            parameters.Add("PipelineName", "tuhngo pipeline");
            parameters.Add("Description", "tuhngo description");
            parameters.Add("SecurityScore", 1);
            parameters.Add("Criticality", CriticalityEnums.Moderate);
            parameters.Add("State", StateEnums.Draft);
            parameters.Add("LastActivity", "Pipeline created");
            parameters.Add("LastActivityTimeStamp", now);
            parameters.Add("CreatedAt", now);
            parameters.Add("UpdatedAt", now);


            var pipelineId = await _persistenceService.QuerySingleOrDefaultAsync<Guid>(CommandPipelineText.InsertPipeline, parameters);
            return pipelineId;
        }

        public async Task<EditPipelineSettingsModel> EditPipelineDetailsAsync(Guid pipelineId, EditPipelineSettingsInputModel editPipelineInputModel)
        {
            return new EditPipelineSettingsModel();
        }

        public async Task<Guid> CreateScratch(Event newEvent)
        {
            var now = DateTime.UtcNow;
            string cmd = @"INSERT INTO Event ([EventLocationId], [EventName], [EventDate], [DateCreated])
			               OUTPUT INSERTED.[Id] 
			               VALUES (@EventLocationId, @EventName, @EventDate, @DateCreated)";
                            
            var parameters = new DynamicParameters();

            parameters.Add("EventLocationId", 1);
            parameters.Add("EventName", "tuhngo-event");
            parameters.Add("EventDate", now);
            parameters.Add("DateCreated", now);


            var eventId = await _persistenceService.QuerySingleOrDefaultAsync<Guid>(cmd, parameters);
            return eventId;
        }
    }
}
