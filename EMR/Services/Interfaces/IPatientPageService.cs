using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IPatientPageService : IBasePageService<Patient>
    {
        public IEnumerable<PatientViewModel> LoadTable(PatientSearchModel searchParameters);
        public IEnumerable<PatientInfoViewModel> LoadPatientInfoTable(PatientInfoSearchModel searchParameters);
        public Patient GetByLogin(string login);

        public Patient GetById(int id);
    }
}
