﻿using EMR.Business.Models;
using System.Collections.Generic;

namespace EMR.Business.Repositories
{
    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="T">Business Model class</typeparam>
    public interface IPatientRepository : IRepository<Patient>
    {
        IEnumerable<Patient> GetByDoctorId(int doctorId);
    }


}
