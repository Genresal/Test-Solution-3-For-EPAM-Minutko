using AutoMapper;
using EMR.Business.Models;
using EMR.ViewModels;

namespace EMR.MappingConfigurations
{
    public class RecordProfile : Profile
    {
        public RecordProfile()
        {
            CreateMap<Record, RecordViewModel>()
                .IncludeMembers(x => x.Doctor, y => y.Patient)
                .ForMember(dest => dest.DoctorName, act => act.MapFrom(src => $"Dr. {src.Doctor.User.FirstName} {src.Doctor.User.LastName}"))
                .ForMember(dest => dest.PatientName, act => act.MapFrom(src => $"{src.Patient.User.FirstName} {src.Patient.User.LastName}"))
                .ForMember(dest => dest.DoctorPosition, act => act.MapFrom(src => $"{src.Doctor.Position.Name} {src.Doctor.Position.Name}"))
                .ForMember(dest => dest.Diagnosis, act => act.MapFrom(src => $"{src.DiagnosisId.ToString("000")} {src.Diagnosis.Name}"));

            CreateMap<Doctor, RecordViewModel>();
            CreateMap<Patient, RecordViewModel>();

            CreateMap<Record, RecordDetailsViewModel>();
                //.IncludeMembers(x => x.Doctor, y => y.Patient, z => z.SickLeave);

            CreateMap<Doctor, RecordDetailsViewModel>();
            CreateMap<Patient, RecordDetailsViewModel>();
            CreateMap<SickLeave, RecordDetailsViewModel>();
        }
    }
}
