using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class DoctorService : BaseBusinessService<Doctor>, IDoctorService
    {
        private readonly IPatientRepository _patientRepository;
        public DoctorService(IRepository<Doctor> doctorRepository, IPatientRepository patientRepository) : base (doctorRepository)
        {
            _patientRepository = patientRepository;
        }
    }
}
