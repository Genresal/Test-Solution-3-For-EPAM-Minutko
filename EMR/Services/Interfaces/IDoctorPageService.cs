using EMR.ViewModels;
using System.Collections.Generic;

namespace EMR.Services
{
    public interface IDoctorPageService : IBasePageService<DoctorViewModel>
    {
        public DoctorViewModel GetByLogin(string login);
        public DoctorViewModel GetByUserId(int userId);
        public IEnumerable<PositionViewModel> GetPositions();
    }
}
