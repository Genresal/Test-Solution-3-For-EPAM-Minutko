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
    public class PatientPageService : IPatientPageService
    {
        readonly IBusinessService<Patient> _patientService;

        public PatientPageService(IBusinessService<Patient> p)
        {
            _patientService = p;
        }

        public PatientViewModel GetById(int id)
        {
            return _patientService.GetById(id).ToViewModel();
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
