using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class PatientService : BaseBusinessService<Patient>
    {
        public PatientService(IRepository<Patient> r) : base (r)
        {
        }
    }
}
