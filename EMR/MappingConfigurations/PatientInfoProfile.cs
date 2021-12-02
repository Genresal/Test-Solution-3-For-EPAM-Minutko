using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class PatientInfoProfile : Profile
    {
        public PatientInfoProfile()
        {
            CreateMap<PatientInfo, PatientInfoViewModel>();
        }
    }
}
