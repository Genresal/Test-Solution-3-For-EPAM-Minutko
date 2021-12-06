using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IPositionPageService : IBasePageService<PositionViewModel>
    {
        public IEnumerable<PositionViewModel> LoadTable(PositionSearchModel searchParameters);
        public bool IsPositionInUse(int positionId);
    }
}
