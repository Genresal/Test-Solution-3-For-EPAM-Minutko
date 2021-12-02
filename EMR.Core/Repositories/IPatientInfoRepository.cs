using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Repositories
{
    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="T">Business Model class</typeparam>
    public interface IPatientInfoRepository
    {
        IEnumerable<PatientInfo> GetPatientsInfo(int doctorId);
    }


}
