using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IDoctorPageService : IBasePageService<Doctor>
    {
        public Doctor GetByLogin(string login);
        public Doctor GetById(int id);
    }
}
