using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IDoctorPageService : IBasePageService<DoctorViewModel>
    {
        public DoctorViewModel GetByLogin(string login);
        public IEnumerable<PositionViewModel> GetPositions();
    }
}
