using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;
        public PatientService(IPatientRepository r, IRepository<User> u, IPatientInfoRepository patientInfoRepository)
        {
            _userRepository = u;
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

        public void Create(Patient model)
        {
            _userRepository.Create(model.User);
            _patientRepository.Create(model);
        }

        public void Update(Patient model)
        {
            _userRepository.Update(model.User);
            _patientRepository.Update(model);
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patientRepository.GetAll();
        }

        public IEnumerable<Patient> GetByColumn(string column, string value)
        {
            return _patientRepository.GetByColumn(column, value);
        }

        public Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }

        public void Delete(int id)
        {
            _patientRepository.Delete(id);
        }
    }
}
