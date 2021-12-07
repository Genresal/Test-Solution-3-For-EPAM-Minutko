using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class SickLeaveProfile : Profile
    {
        public SickLeaveProfile()
        {
            CreateMap<SickLeave, SickLeaveViewModel>()
                .ReverseMap();
        }
    }
}
