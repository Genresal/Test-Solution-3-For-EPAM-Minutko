﻿using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientViewModel>().IncludeMembers(x => x.User)
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ReverseMap();

            CreateMap<User, PatientViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Role, act => act.Ignore());
        }
    }
}