using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IPatientPageService : IBasePageService<PatientViewModel>
    {
        public IEnumerable<PatientInfoViewModel> LoadTable(PatientInfoSearchModel searchParameters);
        public Patient GetByLogin(string login);
    }
}
