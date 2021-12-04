using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class DoctorService : BaseBusinessService<Doctor>, IDoctorService
    {
        public DoctorService(IRepository<Doctor> doctorRepository) : base (doctorRepository)
        {
        }
    }
}
