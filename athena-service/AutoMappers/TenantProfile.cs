﻿using AutoMapper;
using AthenaService.Domain.Models;
using AthenaService.Domain.ViewModels;

namespace AthenaService.AutoMappers
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<TenantModel, TenantViewModel>()
                .ForMember(x => x.CreatedBy, op => op.MapFrom(s => $"{s.CreatedByUser.FirstName} {s.CreatedByUser.LastName}"));
        }
    }
}
