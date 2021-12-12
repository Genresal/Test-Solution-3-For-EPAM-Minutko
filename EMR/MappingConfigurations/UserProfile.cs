using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserViewModel>().IncludeMembers(x => x.Role)
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role.Name))
                .ReverseMap()
                    .ForMember(dest => dest.Role, act => act.Ignore());

            CreateMap<Role, UserViewModel>();
        }
    }
}
