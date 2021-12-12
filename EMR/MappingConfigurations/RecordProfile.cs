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
                .ForMember(dest => dest.DoctorPosition, act => act.MapFrom(src => src.Doctor.Position.Name))
                .ForMember(dest => dest.Diagnosis, act => act.MapFrom(src => src.Diagnosis.Name))
                .ForMember(dest => dest.DiagnosisWithCode, act => act.MapFrom(src => $"{src.DiagnosisId.ToString("000")} {src.Diagnosis.Name}"))
                .ReverseMap()
                    .ForMember(dest => dest.Patient, act => act.Ignore())
                    .ForMember(dest => dest.Doctor, act => act.Ignore())
                    .ForPath(dest => dest.Diagnosis.Name, act => act.MapFrom(act => act.Diagnosis))
                    .ForPath(dest => dest.Diagnosis.Id, act => act.MapFrom(act => act.DiagnosisId));

            CreateMap<Doctor, RecordViewModel>();
            CreateMap<Patient, RecordViewModel>();
            CreateMap<Diagnosis, RecordViewModel>();

            CreateMap<Record, RecordDetailsViewModel>()
                .ForPath(dest => dest.SickLeave.Id, act => act.MapFrom(act => act.SickLeave.Id))
                .ForPath(dest => dest.SickLeave.StartDate, act => act.MapFrom(act => act.SickLeave.StartDate))
                .ForPath(dest => dest.SickLeave.FinalDate, act => act.MapFrom(act => act.SickLeave.FinalDate))
                .ForPath(dest => dest.SickLeave.RecordId, act => act.MapFrom(act => act.Id));
            CreateMap<Doctor, RecordDetailsViewModel>();
            CreateMap<Patient, RecordDetailsViewModel>();
            CreateMap<SickLeave, RecordDetailsViewModel>();

        }
    }
}
