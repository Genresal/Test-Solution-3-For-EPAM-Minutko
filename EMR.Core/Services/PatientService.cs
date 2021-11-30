using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class PatientService : BaseBusinessService<Patient>
    {
        IRepository<User> _userRepository;
        public PatientService(IRepository<Patient> r, IRepository<User> u) : base (r)
        {
            _userRepository = u;
        }

        public override void Create(Patient model)
        {
            _userRepository.Create(model.User);
            _mainRepository.Create(model);
        }

        public override void Update(Patient model)
        {
            _userRepository.Update(model.User);
            _mainRepository.Update(model);
        }
    }
}
