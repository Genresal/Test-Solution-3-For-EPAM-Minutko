using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Mapper;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public class DoctorPageService : IDoctorPageService
    {
        readonly IDoctorService _doctorService;

        public DoctorPageService(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public Doctor GetByLogin(string login)
        {
            return _doctorService.GetByColumn("Login", login).FirstOrDefault();
        }

        public Doctor GetById(int id)
        {
            return _doctorService.GetById(id);
        }

        public void Create(Doctor item)
        {
            _doctorService.Create(item);
        }

        public void Update(Doctor item)
        {
            _doctorService.Update(item);
        }

        public void Delete(int id)
        {
            _doctorService.Delete(id);
        }
    }
}
