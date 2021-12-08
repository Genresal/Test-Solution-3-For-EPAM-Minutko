using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public class PatientService : BaseBusinessService<Patient>, IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;
        public PatientService(IPatientRepository repository
            , IRepository<User> userRepository
            , IPatientInfoRepository patientInfoRepository
            , ILogger<PatientService> logger) : base(repository, logger)
        {
            _patientRepository = repository;
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
