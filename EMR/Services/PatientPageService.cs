using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Helpers;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class PatientPageService : BasePageService<Patient, PatientViewModel>, IPatientPageService
    {
        readonly IPatientService _patientService;

        public PatientPageService(IPatientService patientService, IMapper mapper) : base(patientService, mapper)
        {
            _patientService = patientService;
        }

        public IEnumerable<PatientViewModel> LoadTable(PatientSearchModel searchParameters)
        {
            IEnumerable<Patient> rawResult;

            if (searchParameters.DoctorId > 0)
            {
                rawResult = _patientService.GetByDoctorId(searchParameters.DoctorId);
            }
            else
            {
                rawResult = _patientService.GetAll();
            }

            var result = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientViewModel>>(rawResult);

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.FullName != null && r.FullName.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }

        public IEnumerable<PatientInfoViewModel> LoadPatientInfoTable(PatientInfoSearchModel searchParameters)
        {
            var rawResult = _patientService.GetPatientsInfo(searchParameters.DoctorId);
            var result = _mapper.Map<IEnumerable<PatientInfo>, IEnumerable<PatientInfoViewModel>>(rawResult);

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            if (!string.IsNullOrEmpty(searchParameters.DateRange.Start))
            {
                startDate = DateTime.Parse(searchParameters.DateRange.Start);
            }

            if (!string.IsNullOrEmpty(searchParameters.DateRange.End))
            {
                endDate = DateTime.Parse(searchParameters.DateRange.End);
            }

            if (startDate != DateTime.MinValue)
            {
                result = result.Where(u => u.LastRecordModified >= startDate);
            }

            if (endDate != DateTime.MinValue)
            {
                result = result.Where(u => u.LastRecordModified <= endDate);
            }

            var searchBy = searchParameters.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.FullName != null && r.FullName.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }

        public IEnumerable<Patient> GetByDoctorId(int doctorid)
        {
            return _patientService.GetByDoctorId(doctorid);
        }

        public Patient GetByLogin(string login)
        {
            return _patientService.GetByColumn("Login", login).FirstOrDefault();
        }
    }
}
