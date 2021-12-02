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

        public PatientPageService(IPatientService p)
        {
            _patientService = p;
        }

        public IQueryable<PatientViewModel> LoadTable(PatientSearchModel searchParameters)
        {
            IQueryable<PatientViewModel> result;

            if (searchParameters.DoctorId > 0)
            {
                result = _patientService.GetByDoctorId(searchParameters.DoctorId)
                                        .Select(x => x.ToViewModel())
                                        .AsQueryable();
            }
            else
            {
                result = _patientService.GetAll()
                                        .Select(x => x.ToViewModel())
                                        .AsQueryable();
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
