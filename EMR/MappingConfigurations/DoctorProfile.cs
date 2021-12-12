using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorViewModel>().IncludeMembers(x => x.User)
                .ForMember(dest => dest.Position, act => act.MapFrom(src => src.Position.Name))
                .ReverseMap()
                    .ForMember(dest => dest.Position, act => act.Ignore());

            CreateMap<User, DoctorViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Role, act => act.Ignore());



            CreateMap<Doctor, DoctorEditViewModel>().IncludeMembers(x => x.User)
        .ForMember(dest => dest.Position, act => act.MapFrom(src => src.Position.Name))
        .ReverseMap()
            .ForMember(dest => dest.Position, act => act.Ignore());

            CreateMap<User, DoctorEditViewModel>()
                    .ReverseMap()
                    .ForMember(dest => dest.Role, act => act.Ignore());
        }
    }
}
