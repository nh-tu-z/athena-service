using AutoMapper;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;

namespace AthenaService.AutoMappers
{
    public class TenantProvisionTaskProfile : Profile
    {
        public TenantProvisionTaskProfile()
        {
            CreateMap<TenantProvisionTaskModel, TenantProvisionTaskViewModel>();
        }
    }
}
