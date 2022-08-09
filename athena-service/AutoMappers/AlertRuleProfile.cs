using AutoMapper;
using AthenaService.Domain.ViewModels;
using AthenaService.Domain.Models;

namespace AthenaService.AutoMappers
{
    public class AlertRuleProfile : Profile
    {
        public AlertRuleProfile()
        {
            CreateMap<AlertRuleModel, AlertRuleViewModel>();
        }
    }
}
