using AutoMapper;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class RegisterViewModelProfile : Profile
    {
        public RegisterViewModelProfile()
        {
            CreateMap<RegisterViewModel, UserViewModel>();
            CreateMap<RegisterViewModel, DoctorViewModel>();
            CreateMap<RegisterViewModel, PatientViewModel>();
        }
    }
}
