using EMR.Business.Models;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface IPatientService : IBusinessService<Patient>
    {
        IEnumerable<PatientInfo> GetPatientsInfo(int doctorId);
    }


}
