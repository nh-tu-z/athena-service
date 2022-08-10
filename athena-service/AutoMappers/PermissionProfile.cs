using AutoMapper;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;

namespace AthenaService.AutoMappers
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionModel, PermissionViewModel>();
            CreateMap<UserPermissionModel, UserPermissionViewModel>();
        }
    }
}
