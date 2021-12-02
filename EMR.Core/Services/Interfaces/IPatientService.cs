using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    public interface IPatientService : IBusinessService<Patient>
    {
        IEnumerable<Patient> GetByDoctorId(int doctorId);
    }


}
