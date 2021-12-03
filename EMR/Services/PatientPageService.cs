using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Mapper;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public class PatientPageService : BaseTableService, IPatientPageService
    {
        readonly IPatientService _patientService;
        private readonly IMapper _mapper;


        public PatientPageService(IPatientService p, IMapper mapper)
        {
            _patientService = p;
            _mapper = mapper;
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

            result = Order(searchParameters, result);

            return result;
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

            result = Order(searchParameters, result);

            return result;
        }

        public IEnumerable<Patient> GetByDoctorId(int doctorid)
        {
            return _patientService.GetByDoctorId(doctorid);
        }

        public Patient GetByLogin(string login)
        {
            return _patientService.GetByColumn("Login", login).FirstOrDefault();
        }

        public Patient GetById(int id)
        {
            return _patientService.GetById(id);
        }

        public void Create(Patient item)
        {
            _patientService.Create(item);
        }

        public void Update(Patient item)
        {
            _patientService.Update(item);
        }

        public void Delete(int id)
        {
            _patientService.Delete(id);
        }
    }
}
