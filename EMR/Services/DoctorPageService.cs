using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using System.Linq;

namespace EMR.Services
{
    public class DoctorPageService : BasePageService<Doctor, DoctorViewModel>, IDoctorPageService
    {
        public DoctorPageService(IDoctorService doctorService, IMapper mapper) : base(doctorService, mapper)
        {
        }

        public DoctorViewModel GetByLogin(string login)
        {
            var model = _mainService.GetByColumn(nameof(User.Login), login).FirstOrDefault();
            return _mapper.Map<Doctor, DoctorViewModel>(model);
        }
    }
}
