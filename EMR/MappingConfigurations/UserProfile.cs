using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.MappingConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserViewModel>().IncludeMembers(x => x.Role)
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role.Name));

            CreateMap<Role, UserViewModel>();
        }
    }
}
