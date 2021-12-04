using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class DrugProfile : Profile
    {
        public DrugProfile()
        {
            CreateMap<Drug, DrugViewModel>()
                .ReverseMap();
        }
    }
}
