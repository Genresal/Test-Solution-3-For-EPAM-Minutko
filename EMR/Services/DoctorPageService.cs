using AutoMapper;
using EMR.Business.Helpers;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class DoctorPageService : BasePageService<Doctor, DoctorViewModel>, IDoctorPageService
    {
        private readonly IPositionService _positionService;
        public DoctorPageService(IDoctorService doctorService, IPositionService positionService, IMapper mapper) : base(doctorService, mapper)
        {
            _positionService = positionService;
        }

        public override void Create(DoctorViewModel viewModel)
        {
            var model = _mapper.Map<DoctorViewModel, Doctor>(viewModel);
            model.User.Password = model.User.Password.HashString();
            _mainService.Create(model);
        }

        public DoctorViewModel GetByLogin(string login)
        {
            var model = _mainService.GetByColumn(nameof(User.Login), login).FirstOrDefault();
            return _mapper.Map<Doctor, DoctorViewModel>(model);
        }

        public DoctorViewModel GetByUserId(int userId)
        {
            var model = _mainService.GetByColumn(nameof(Doctor.UserId), userId.ToString()).FirstOrDefault();
            return _mapper.Map<Doctor, DoctorViewModel>(model);
        }

        public IEnumerable<PositionViewModel> GetPositions()
        {
            var result = _positionService.GetAll();
            return _mapper.Map<IEnumerable<Position>, IEnumerable<PositionViewModel>>(result);
        }
    }
}
