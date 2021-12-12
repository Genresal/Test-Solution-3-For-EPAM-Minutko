using EMR.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EMR.Services
{
    public interface IDoctorPageService : IBasePageService<DoctorViewModel>
    {
        public DoctorViewModel GetByLogin(string login);
        public DoctorViewModel GetByUserId(int userId);
        public List<SelectListItem> PreparePositions();

        public DoctorEditViewModel GetByIdEditModel(int id);
        public void Create(DoctorEditViewModel viewModel);
        public void Update(DoctorEditViewModel viewModel);
    }
}
