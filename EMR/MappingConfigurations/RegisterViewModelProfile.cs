using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
