using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class DoctorPageService : BasePageService<Doctor, DoctorViewModel>, IDoctorPageService
    {
        private readonly IBusinessService<Position> _positionService;
        public DoctorPageService(IDoctorService doctorService, IBusinessService<Position> positionService, IMapper mapper) : base(doctorService, mapper)
        {
            _positionService = positionService;
        }

        public DoctorViewModel GetByLogin(string login)
        {
            var model = _mainService.GetByColumn(nameof(User.Login), login).FirstOrDefault();
            return _mapper.Map<Doctor, DoctorViewModel>(model);
        }

        public override void Create (DoctorViewModel viewModel)
        {
            var doctorModel = _mapper.Map<DoctorViewModel, Doctor>(viewModel);
            _mainService.Create(doctorModel);
        }

        public IEnumerable<PositionViewModel> GetPositions()
        {
            var result = _positionService.GetAll();
            return _mapper.Map<IEnumerable<Position>, IEnumerable<PositionViewModel>>(result);
        }
    }
}
