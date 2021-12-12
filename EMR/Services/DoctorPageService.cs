using AutoMapper;
using EMR.Business.Helpers;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public void Create(DoctorEditViewModel viewModel)
        {
            var model = _mapper.Map<DoctorEditViewModel, Doctor>(viewModel);
            model.User.Password = model.User.Password.HashString();
            _mainService.Create(model);
        }

        public virtual void Update(DoctorEditViewModel viewModel)
        {
            var model = _mapper.Map<DoctorEditViewModel, Doctor>(viewModel);
            _mainService.Update(model);
        }

        public virtual DoctorEditViewModel GetByIdEditModel(int id)
        {
            var rawResult = _mainService.GetById(id);
            var result = _mapper.Map<Doctor, DoctorEditViewModel>(rawResult);
            result.Positions = PreparePositions();
            return result;
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

        public List<SelectListItem> PreparePositions()
        {
            var rawResult = _positionService.GetAll();
            var result = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionViewModel>>(rawResult);

            List<SelectListItem> positions = new List<SelectListItem>();
            positions.AddRange(result
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList());

            return positions;
        }
    }
}
