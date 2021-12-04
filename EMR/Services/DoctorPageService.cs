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
    public class DoctorPageService : BasePageService<Doctor>, IDoctorPageService
    {
        public DoctorPageService(IDoctorService doctorService) : base(doctorService)
        {
        }

        public Doctor GetByLogin(string login)
        {
            return _mainService.GetByColumn(nameof(User.Login), login).FirstOrDefault();
        }
    }
}
