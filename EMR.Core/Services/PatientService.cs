using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public class PatientService : BaseBusinessService<Patient>, IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;
        public PatientService(IRepository<Patient> repository
            , IRepository<User> userRepository
            , IPatientInfoRepository patientInfoRepository
            , ILogger<PatientService> logger) : base(repository, logger)
        {
            _patientRepository = repository;
            _patientInfoRepository = patientInfoRepository;
        }

        public IEnumerable<PatientInfo> GetPatientsInfo(int doctorId)
        {
            return _patientInfoRepository.GetPatientsInfo(doctorId);
        }
    }
}
