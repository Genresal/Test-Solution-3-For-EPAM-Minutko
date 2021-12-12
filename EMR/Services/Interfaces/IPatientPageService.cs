using EMR.ViewModels;
using System.Collections.Generic;

namespace EMR.Services
{
    public interface IPatientPageService : IBasePageService<PatientViewModel>
    {
        public IEnumerable<PatientInfoViewModel> LoadTable(PatientInfoSearchModel searchParameters);
        public PatientViewModel GetByLogin(string login);
        public PatientViewModel GetByUserId(int userId);
    }
}
