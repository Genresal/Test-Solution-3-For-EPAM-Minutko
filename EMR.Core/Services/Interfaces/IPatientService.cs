using EMR.Business.Models;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface IPatientService : IBusinessService<Patient>
    {
        IEnumerable<Patient> GetByDoctorId(int doctorId);
        IEnumerable<PatientInfo> GetPatientsInfo(int doctorId);
    }


}
