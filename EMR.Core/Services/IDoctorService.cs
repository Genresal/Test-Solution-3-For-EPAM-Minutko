using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
        //Doctor FindById(int id);
        void Create(Doctor model);
        void Update(Doctor Model);
        void Delete(int id);
    }
}