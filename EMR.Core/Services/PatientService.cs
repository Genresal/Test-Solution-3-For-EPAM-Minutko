using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class PatientService : BaseBusinessService<Patient>, IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;
        public PatientService(IPatientRepository r, IRepository<User> u, IPatientInfoRepository patientInfoRepository) : base(r)
        {
            _patientRepository = r;
            _patientInfoRepository = patientInfoRepository;
        }

        public IEnumerable<Patient> GetByDoctorId(int doctorId)
        {
            return _patientRepository.GetByDoctorId(doctorId);
        }

        public IEnumerable<PatientInfo> GetPatientsInfo(int doctorId)
        {
            return _patientInfoRepository.GetPatientsInfo(doctorId);
        }
    }
}
