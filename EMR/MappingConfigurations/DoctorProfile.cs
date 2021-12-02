using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.MappingConfigurations
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            //https://docs.automapper.org/en/stable/Flattening.html
            //https://www.generacodice.com/en/articolo/3119123/automapper-map-from-nested-class-to-single-(flatten)
            
            CreateMap<Doctor, DoctorViewModel>().IncludeMembers(x => x.User)
                .ForMember(dest => dest.Position, act => act.MapFrom(src => src.Position.Name));

            CreateMap<User, DoctorViewModel>();
        }
    }
}
