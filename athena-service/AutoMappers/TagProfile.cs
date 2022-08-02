using AutoMapper;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;

namespace AthenaService.AutoMappers
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagModel, TagViewModel>();
        }
    }
}
