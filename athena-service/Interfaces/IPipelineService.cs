using AthenaService.Common.Events.Requests;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;
using AthenaService.Domain.Pipelines.Entities;

using AthenaService.Scratch;

namespace AthenaService.Interfaces
{
    public interface IPipelineService
    {
        Task<Pipeline> GetPipelineByIdAsync(Guid pipelineId);
        Task<IList<PipelineModel>> GetAllAsync(GetAllPipelineRequest request);
        Task<Guid> CreatePipelineAsync(CreatePipelineDetailInputModel createPipelineInputModel);
        Task<EditPipelineSettingsModel> EditPipelineDetailsAsync(Guid pipelineId, EditPipelineSettingsInputModel editPipelineInputModel);
        Task<Guid> CreateScratch(Event newEvent);
    }
}
