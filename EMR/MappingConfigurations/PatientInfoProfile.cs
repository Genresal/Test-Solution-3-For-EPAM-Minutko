using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class PatientInfoProfile : Profile
    {
        public PatientInfoProfile()
        {
            CreateMap<PatientInfo, PatientInfoViewModel>()
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
