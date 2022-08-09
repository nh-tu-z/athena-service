using AutoMapper;

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
