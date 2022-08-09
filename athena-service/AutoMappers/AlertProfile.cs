using AutoMapper;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;

namespace AthenaService.AutoMappers
{
    public class AlertProfile : Profile
    {
        public AlertProfile()
        {
            CreateMap<AlertModel, AlertViewModel>();
        }
    }
}
